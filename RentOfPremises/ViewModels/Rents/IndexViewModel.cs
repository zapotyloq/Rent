using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Rents
{
    public class IndexViewModel
    {
        public IEnumerable<Models.Rent> Rents { get; set; }
        public IEnumerable<Models.Premise> Premises { get; set; }
        public IEnumerable<Models.Organization> Organizations { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
