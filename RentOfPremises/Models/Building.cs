using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentOfPremises.Models
{
    public class Building
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public int NumberOfStoreys { get; set; }
        public string Characteristic { get; set; }

        public virtual ICollection<Premises> Premises { get; set; }
    }
}
