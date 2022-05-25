using eShopApp.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

 
        public IFormFile Picture { get; set; }
        public string PictureStr { get; set; }


        #region Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }

        #endregion
    }
}
