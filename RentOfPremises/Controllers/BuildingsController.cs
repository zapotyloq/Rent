using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Buildings;
using Microsoft.EntityFrameworkCore;

namespace RentOfPremises.Controllers
{
    public class BuildingsController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public async Task<IActionResult> Index(int? id, string name, int page = 1,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Building> source = db.Buildings;

            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                case SortState.MailAsc:
                    source = source.OrderBy(s => s.Mail);
                    break;
                case SortState.MailDesc:
                    source = source.OrderByDescending(s => s.Mail);
                    break;
                case SortState.NumberOfStoreysAsc:
                    source = source.OrderBy(s => s.NumberOfStoreys);
                    break;
                case SortState.NumberOfStoreysDesc:
                    source = source.OrderByDescending(s => s.NumberOfStoreys);
                    break;
                case SortState.CharasteristicAsc:
                    source = source.OrderBy(s => s.Characteristic);
                    break;
                case SortState.CharasteristicDesc:
                    source = source.OrderByDescending(s => s.Characteristic);
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
                Buildings = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Buildings.ToList(), id, name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(string name, string mail, int numberOfStoreys, string charasteristic)
        {
            Building building = new Building
            {
                Name = name,
                Mail = mail,
                NumberOfStoreys = numberOfStoreys,
                Characteristic = charasteristic
            };
            db.Buildings.Add(building);
            db.SaveChanges();
            JsonResult data = Json(building);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Building building = null;
            try
            {
                building = db.Buildings.Where(c => c.Id == id).First();
                db.Buildings.Remove(building);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(building);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string mail, int numberOfStoreys, string charasteristic)
        {
            Building building = null;
            try
            {
                building = db.Buildings.Where(c => c.Id == id).First();
                building.Name = name;
                building.Mail = mail;
                building.NumberOfStoreys = numberOfStoreys;
                building.Characteristic = charasteristic;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(building);
            return data;
        }
    }
}