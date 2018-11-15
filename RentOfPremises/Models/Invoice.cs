using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("RentId")]
        public virtual Rent Rent { get; set; }
    }
}
