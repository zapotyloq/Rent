﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentOfPremises.Models
{
    public class Premises
    {
        public int Id { get; set; }
        public int Area { get; set; }
        public int BuildingNumber { get; set; }
        public string FloorPlan { get; set; }
        public string Photos { get; set; }
    }
}