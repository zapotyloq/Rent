using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Premises;
using Microsoft.EntityFrameworkCore;
using RentOfPremises.Infrastructure;
using RentOfPremises.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;

namespace RentOfPremises.Controllers
{
    [Authorize(Roles = "user")]
    public class PremisesController : Controller
    {
        ApplicationContext db;

        public PremisesController(ApplicationContext db)
        {
            this.db = db;
        }
        
        [SetToSession("Premises")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            var sessionOrganizations = HttpContext.Session.Get("Premises");
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

            IQueryable<Premise> source = db.Premises;
            await source.ForEachAsync(d => d.Building = db.Buildings.Where(p => p.Id == d.BuildingNumber).First());

            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Building.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.AreaDesc:
                    source = source.OrderByDescending(s => s.Area);
                    break;
                case SortState.AreaAsc:
                    source = source.OrderBy(s => s.Area);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.BuildingNameAsc:
                    source = source.OrderBy(s => s.Building.Name);
                    break;
                case SortState.BuildingNameDesc:
                    source = source.OrderByDescending(s => s.Building.Name);
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
                Premises = items,
                Buildings = db.Buildings,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Premises.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(int area, int buildingNumber, string floorPlan, string photos)
        {
            Premise premise = new Premise
            {
                Area = area,
                BuildingNumber = buildingNumber,
                Building = db.Buildings.Where(p => p.Id == buildingNumber).First(),
                FloorPlan = floorPlan,
                Photos = photos
            };
            db.Premises.Add(premise);
            db.SaveChanges();
            JsonResult data = Json(premise);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Premise premise = null;
            try
            {
                premise = db.Premises.Where(c => c.Id == id).First();
                db.Premises.Remove(premise);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(premise);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, int area, int buildingNumber, string floorPlan, string photos)
        {
            Premise premise = null;
            try
            {
                premise = db.Premises.Where(c => c.Id == id).First();
                premise.Area = area;
                premise.BuildingNumber = buildingNumber;
                premise.Building = db.Buildings.Where(p => p.Id == buildingNumber).First();
                premise.FloorPlan = floorPlan;
                premise.Photos = photos;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(premise);
            return data;
        }
    }
}