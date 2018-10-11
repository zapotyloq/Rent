using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Organizations
{
    public class FilterViewModel
    {
        public SelectList Organizations { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Organization> organizations, int? id, string name)
        {
            organizations.Insert(0, new Organization { Name = "Все", Id = 0 });
            Organizations = new SelectList(organizations, "Id", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
