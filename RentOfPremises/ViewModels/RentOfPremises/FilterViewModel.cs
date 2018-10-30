using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.RentOfPremises
{
    public class FilterViewModel
    {
        public SelectList RentOfPremises { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.RentOfPremises> rents, int? id, string name)
        {
            rents.Insert(0, new Models.RentOfPremises { Id = 0 });
            RentOfPremises = new SelectList(rents, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
