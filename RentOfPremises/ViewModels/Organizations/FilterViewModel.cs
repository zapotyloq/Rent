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
        public SelectList Objects { get; private set; }
        public SelectList Buildings { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set;}
        public string FirstDate { get; private set; }
        public string SecondDate { get; private set; }

        public FilterViewModel(List<Organization> organizations, int? id, string name)
        {
            organizations.Insert(0, new Organization { Name = "Все", Id = 0 });
            Organizations = new SelectList(organizations, "Id", "Name", id);
            SelectedId = id;
            SelectedName = name;
        }

        public FilterViewModel(List<object[]> objects, string buildingName, string d0, string d, List<Building> buildings)
        {
            objects.Insert(0, new object[]{"",""});
            Objects = new SelectList(objects);
            SelectedName = buildingName;
            FirstDate = d0;
            SecondDate = d;
            Buildings = new SelectList(buildings, "Name", "Name", 0);
        }

        public FilterViewModel(List<object[]> objects, string d)
        {
            objects.Insert(0, new object[] { "", "" });
            Objects = new SelectList(objects);
            FirstDate = d;
        }
    }
}
