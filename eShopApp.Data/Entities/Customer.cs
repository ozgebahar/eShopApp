using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopApp.Data.Entities
{
   public class Customer : User
    {
        [Required]
        [StringLength(50)]
        public string County { get; set; }
 
        [Required]
        [StringLength(50)]
        public string District { get; set; }

        [Required]
        [StringLength(100)]
        public string HomeAddress { get; set; }
    }
}
