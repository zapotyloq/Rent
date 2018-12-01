using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Rents;
using Microsoft.EntityFrameworkCore;
using RentOfPremises.Infrastructure;
using RentOfPremises.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;

namespace RentOfPremises.Controllers
{
    [Authorize(Roles = "user")]
    public class RentsController : Controller
    {
        ApplicationContext db;

        public RentsController(ApplicationContext db)
        {
            this.db = db;
        }

        [SetToSession("Rents")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            var sessionOrganizations = HttpContext.Session.Get("Rents");
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

            IQueryable<Models.Rent> source = db.Rents;
            await source.ForEachAsync(d => d.Premise = db.Premises.Where(p => p.Id == d.PremiseId).First());
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
                case SortState.PremiseIdDesc:
                    source = source.OrderByDescending(s => s.PremiseId);
                    break;
                case SortState.PremiseIdAsc:
                    source = source.OrderBy(s => s.PremiseId);
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
                FilterViewModel = new FilterViewModel(db.Rents.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(int premiseId, int organizationId, DateTime arrivalDate, DateTime dateOfDeparture)
        {
            Models.Rent rent = new Models.Rent
            {
                PremiseId = premiseId,
                OrganizationId = organizationId,
                ArrivalDate = arrivalDate,
                DateOfDeparture = dateOfDeparture,

                Organization = db.Organizations.Where(p => p.Id == organizationId).First(),
                Premise = db.Premises.Where(p => p.Id == premiseId).First()
            };
            db.Rents.Add(rent);
            db.SaveChanges();
            JsonResult data = Json(rent);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Models.Rent rent = null;
            try
            {
                rent = db.Rents.Where(c => c.Id == id).First();
                db.Rents.Remove(rent);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(rent);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, int premiseId, int organizationId, DateTime arrivalDate, DateTime dateOfDeparture)
        {
            Models.Rent rent = null;
            try
            {
                rent = db.Rents.Where(c => c.Id == id).First();
                rent.PremiseId = premiseId;
                rent.OrganizationId = organizationId;
                rent.ArrivalDate = arrivalDate;
                rent.DateOfDeparture = dateOfDeparture;

                rent.Organization = db.Organizations.Where(p => p.Id == organizationId).First();
                rent.Premise = db.Premises.Where(p => p.Id == premiseId).First();
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(rent);
            return data;
        }
    }
}