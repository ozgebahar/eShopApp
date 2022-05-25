﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopApp.Data.Entities
{
   public class OrderDetail 
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
