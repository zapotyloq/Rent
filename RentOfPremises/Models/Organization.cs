using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<RentOfPremises> RentOfPremises { get; set; }
        public Organization()
        {
            RentOfPremises = new List<RentOfPremises>();
        }
    }
}
