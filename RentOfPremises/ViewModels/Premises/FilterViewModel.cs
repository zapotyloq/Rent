using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Premises
{
    public class FilterViewModel
    {
        public SelectList Premises { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Premises> premises, int? id, string name)
        {
            premises.Insert(0, new Models.Premises { Id = 0 });
            Premises = new SelectList(premises, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
