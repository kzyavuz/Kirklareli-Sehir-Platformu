﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Pharmacy
    {
        [Key]
        public int ID { get; set; }
        public string PharmacyName { get; set; }
        public string District { get; set; }
        public string StreetAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string GoogleLocal { get; set; }

    }
}
