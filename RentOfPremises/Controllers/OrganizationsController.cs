using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using RentOfPremises.ViewModels;
using RentOfPremises.ViewModels.Organizations;
using Microsoft.EntityFrameworkCore;

namespace RentOfPremises.Controllers
{
    public class OrganizationsController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public async Task<IActionResult> Index(int? id, string name, int page = 1,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Organization> source = db.Organizations;

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
                Organizations = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Organizations.ToList(), id, name)
            };
            return View(viewModel);
        }
    }
}