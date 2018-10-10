using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.Models
{
    public class OrganizationViewModel
    {
        public IEnumerable<Organization> Organizations { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
