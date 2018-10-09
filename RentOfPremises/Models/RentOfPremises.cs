using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.Models
{
    public class RentOfPremises
    {
        public int Id { get; set; }
        public int PremisesId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DateOfDeparture { get; set; }
    }
}
