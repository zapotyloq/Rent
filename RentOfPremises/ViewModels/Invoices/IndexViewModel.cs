using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentOfPremises.Models;

namespace RentOfPremises.ViewModels.Invoices
{
    public class IndexViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Models.RentOfPremises> Rents { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
