using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Rents
{
    public class FilterViewModel
    {
        public SelectList Rents { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Rent> rents, int? id, string name)
        {
            rents.Insert(0, new Models.Rent { Id = 0 });
            Rents = new SelectList(rents, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
