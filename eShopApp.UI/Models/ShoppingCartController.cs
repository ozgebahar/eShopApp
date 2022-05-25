using eShopApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Models
{
    public class ShoppingCartController
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotalPrice
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }
    }
}
