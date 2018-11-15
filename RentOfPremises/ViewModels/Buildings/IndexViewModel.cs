using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Buildings
{
    public class IndexViewModel
    {
        public IEnumerable<Building> Buildings { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
