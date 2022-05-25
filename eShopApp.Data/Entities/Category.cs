using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopApp.Data.Entities
{
   public class Category : BaseEntity
    {

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }


        #region Releations
        public List<Product> Products { get; set; }
        #endregion
    }
}
