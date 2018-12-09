﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carpentry.Core.Models
{
    public class Carpenter
    {
        public string Id { get; set; }

        [StringLength(20)]
        [DisplayName("Carpenter Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0,1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string AssociatedVendor { get; set; }


        public Carpenter()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }

}