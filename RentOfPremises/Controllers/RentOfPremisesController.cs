using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.RentOfPremises;
using Microsoft.EntityFrameworkCore;

namespace RentOfPremises.Controllers
{
    public class RentOfPremisesController : Controller
    {
        ApplicationContext db;

        public RentOfPremisesController(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(int? id, string name, int page = 1,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Models.RentOfPremises> source = db.RentOfPremises;
            await source.ForEachAsync(d => d.Premises = db.Premises.Where(p => p.Id == d.PremisesId).First());
            await source.ForEachAsync(d => d.Organization = db.Organizations.Where(p => p.Id == d.OrganizationId).First());


            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Organization.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.PremisesIdDesc:
                    source = source.OrderByDescending(s => s.PremisesId);
                    break;
                case SortState.PremisesIdAsc:
                    source = source.OrderBy(s => s.PremisesId);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.OrganizationNameAsc:
                    source = source.OrderBy(s => s.Organization.Name);
                    break;
                case SortState.OrganizationNameDesc:
                    source = source.OrderByDescending(s => s.Organization.Name);
                    break;
                case SortState.ArrivalDateAsc:
                    source = source.OrderBy(s => s.ArrivalDate);
                    break;
                case SortState.ArrivalDateDesc:
                    source = source.OrderByDescending(s => s.ArrivalDate);
                    break;
                case SortState.DateOfDepartureAsc:
                    source = source.OrderBy(s => s.DateOfDeparture);
                    break;
                case SortState.DateOfDepartureDesc:
                    source = source.OrderByDescending(s => s.DateOfDeparture);
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
                Rents = items,
                Organizations = db.Organizations,
                Premises = db.Premises,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.RentOfPremises.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(int premisesId, int organizationId, DateTime arrivalDate, DateTime dateOfDeparture)
        {
            Models.RentOfPremises rent = new Models.RentOfPremises
            {
                PremisesId = premisesId,
                OrganizationId = organizationId,
                ArrivalDate = arrivalDate,
                DateOfDeparture = dateOfDeparture,

                Organization = db.Organizations.Where(p => p.Id == organizationId).First(),
                Premises = db.Premises.Where(p => p.Id == premisesId).First()
            };
            db.RentOfPremises.Add(rent);
            db.SaveChanges();
            JsonResult data = Json(rent);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.RentOfPremises rent = null;
            try
            {
                rent = db.RentOfPremises.Where(c => c.Id == id).First();
                db.RentOfPremises.Remove(rent);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(rent);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, int premisesId, int organizationId, DateTime arrivalDate, DateTime dateOfDeparture)
        {
            Models.RentOfPremises rent = null;
            try
            {
                rent = db.RentOfPremises.Where(c => c.Id == id).First();
                rent.PremisesId = premisesId;
                rent.OrganizationId = organizationId;
                rent.ArrivalDate = arrivalDate;
                rent.DateOfDeparture = dateOfDeparture;

                rent.Organization = db.Organizations.Where(p => p.Id == organizationId).First();
                rent.Premises = db.Premises.Where(p => p.Id == premisesId).First();
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(rent);
            return data;
        }
    }
}