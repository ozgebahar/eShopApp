using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopApp.Data.Entities
{
   public class Product : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool Discontinued { get; set; } = true;
        public byte[] Picture { get; set; }

        #region Releations
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}
