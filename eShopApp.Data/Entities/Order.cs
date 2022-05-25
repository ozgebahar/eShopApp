using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopApp.Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipAddress { get; set; }


        #region Releations
        public List<OrderDetail> OrderDetails { get; set; } 
        #endregion
    }
}
