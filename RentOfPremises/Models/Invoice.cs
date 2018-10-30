using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime DateOfPayment { get; set; }
        public int RentId { get; set; }
        public int Mounth { get; set; }
        public double Total { get; set; }
        public string Bailee { get; set; }

        public virtual RentOfPremises RentOfPremises { get; set; }
    }
}
