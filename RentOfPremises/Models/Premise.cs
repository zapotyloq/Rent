using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentOfPremises.Models
{
    public class Premise
    {
        public int Id { get; set; }
        public int Area { get; set; }
        public int BuildingNumber { get; set; }
        public string FloorPlan { get; set; }
        public string Photos { get; set; }

        public virtual Building Building { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
