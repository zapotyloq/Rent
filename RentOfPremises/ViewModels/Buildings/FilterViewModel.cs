using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Buildings
{
    public class FilterViewModel
    {
        public SelectList Buildings { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Building> buildings, int? id, string name)
        {
            buildings.Insert(0, new Building { Name = "Все", Id = 0 });
            Buildings = new SelectList(buildings, "Id", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
