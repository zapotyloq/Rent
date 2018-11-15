using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Invoices;
using Microsoft.EntityFrameworkCore;
using RentOfPremises.Infrastructure;
using RentOfPremises.Infrastructure.Filters;

namespace RentOfPremises.Controllers
{
    public class InvoicesController : Controller
    {
        ApplicationContext db;

        public InvoicesController(ApplicationContext db)
        {
            this.db = db;
        }

        [SetToSession("Invoices")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            var sessionOrganizations = HttpContext.Session.Get("Invoices");
            if (sessionOrganizations != null && id == null && name == null && page == 0 && sortOrder == SortState.IdAsc)
            {
                if (sessionOrganizations.Keys.Contains("id"))
                    id = Convert.ToInt32(sessionOrganizations["id"]);
                if (sessionOrganizations.Keys.Contains("name"))
                    name = sessionOrganizations["name"];
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            }

            if(page == 0)
            {
                page = 1;
            }
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Invoice> source = db.Invoices;
            await source.ForEachAsync(d => d.Rent = db.Rents.Where(p => p.Id == d.RentId).First());


            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Bailee.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.DateOfPaymentDesc:
                    source = source.OrderByDescending(s => s.DateOfPayment);
                    break;
                case SortState.DateOfPaymentAsc:
                    source = source.OrderBy(s => s.DateOfPayment);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.RentIdAsc:
                    source = source.OrderBy(s => s.RentId);
                    break;
                case SortState.RentIdDesc:
                    source = source.OrderByDescending(s => s.RentId);
                    break;
                case SortState.MounthAsc:
                    source = source.OrderBy(s => s.Mounth);
                    break;
                case SortState.MounthDesc:
                    source = source.OrderByDescending(s => s.Mounth);
                    break;
                case SortState.TotalAsc:
                    source = source.OrderBy(s => s.Total);
                    break;
                case SortState.TotalDesc:
                    source = source.OrderByDescending(s => s.Total);
                    break;
                default:
                    source = source.OrderBy(s => s.Id);
                    break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Invoices = items,
                Rents = db.Rents,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Invoices.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(DateTime dateOfPayment, int rentId, int mounth, double total, string bailee)
        {
            Invoice invoice = new Invoice
            {
                DateOfPayment = dateOfPayment,
                RentId = rentId,
                Mounth = mounth,
                Total = total,
                Bailee = bailee,

                Rent = db.Rents.Where(p => p.Id == rentId).First(),
            };
            db.Invoices.Add(invoice);
            db.SaveChanges();
            JsonResult data = Json(invoice);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Invoice invoice = null;
            try
            {
                invoice = db.Invoices.Where(c => c.Id == id).First();
                db.Invoices.Remove(invoice);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(invoice);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, DateTime dateOfPayment, int rentId, int mounth, double total, string bailee)
        {
            Invoice invoice = null;
            try
            {
                invoice = db.Invoices.Where(c => c.Id == id).First();
                invoice.DateOfPayment = dateOfPayment;
                invoice.RentId = rentId;
                invoice.Mounth = mounth;
                invoice.Total = total;
                invoice.Bailee = bailee;

                invoice.Rent = db.Rents.Where(p => p.Id == rentId).First();
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(invoice);
            return data;
        }
    }
}