using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Premises;
using Microsoft.EntityFrameworkCore;

namespace RentOfPremises.Controllers
{
    public class PremisesController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public async Task<IActionResult> Index(int? id, string name, int page = 1,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Premises> source = db.Premises;
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
            Premises premises = new Premises
            {
                Area = area,
                BuildingNumber = buildingNumber,
                Building = db.Buildings.Where(p => p.Id == buildingNumber).First(),
                FloorPlan = floorPlan,
                Photos = photos
            };
            db.Premises.Add(premises);
            db.SaveChanges();
            JsonResult data = Json(premises);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Premises premises = null;
            try
            {
                premises = db.Premises.Where(c => c.Id == id).First();
                db.Premises.Remove(premises);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(premises);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, int area, int buildingNumber, string floorPlan, string photos)
        {
            Premises premises = null;
            try
            {
                premises = db.Premises.Where(c => c.Id == id).First();
                premises.Area = area;
                premises.BuildingNumber = buildingNumber;
                premises.Building = db.Buildings.Where(p => p.Id == buildingNumber).First();
                premises.FloorPlan = floorPlan;
                premises.Photos = photos;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(premises);
            return data;
        }
    }
}