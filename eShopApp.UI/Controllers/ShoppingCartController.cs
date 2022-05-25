using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ShoppingCartController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
