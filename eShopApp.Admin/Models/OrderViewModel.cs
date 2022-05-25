using eShopApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Models
{
    public class OrderViewModel
    {

        public int Id { get; set; }
        public bool IsActive { get; set; }

        public DateTime OrderDate { get; set; }


        [Required]
        [StringLength(500)]
        public string ShipAddress { get; set; }

        #region Relations
        public int UserId { get; set; }
        public User User { get; set; } 
        #endregion
    }
}
