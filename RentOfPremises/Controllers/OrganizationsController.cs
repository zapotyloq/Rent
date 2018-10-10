using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentOfPremises.Models;
using Microsoft.EntityFrameworkCore;

namespace RentOfPremises.Controllers
{
    public class OrganizationsController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public async Task<IActionResult> Organizations(int page = 1)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Organization> source = db.Organizations;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            OrganizationViewModel viewModel = new OrganizationViewModel
            {
                PageViewModel = pageViewModel,
                Organizations = items
            };
            return View(viewModel);
        }
    }
}