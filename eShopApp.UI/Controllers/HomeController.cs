using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using eShopApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        public HomeController(IRepository<Category> categoryRepository, IRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {

            var indexViewModel = new IndexViewModel();

            var categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new CategoryViewModel()
            {
                CategoryName = x.CategoryName,
                Description = x.Description,
                Id = x.Id,
                PictureStr = Convert.ToBase64String(x.Picture)

            }).ToList();

            indexViewModel.Categories = categories;

            var products = _productRepository.GetAll(include: x => x.Include(y => y.Category)).Select(x =>
               new ProductViewModel()
               {
                   Id = x.Id,
                   ProductName = x.ProductName,
                   UnitPrice = x.UnitPrice,
                   UnitsInStock = x.UnitsInStock,
                   Discontinued = x.Discontinued,
                   CategoryId = x.Category.Id,
                   PictureStr = Convert.ToBase64String(x.Picture)
               }).ToList();

            indexViewModel.Products = products;

            return View(indexViewModel);
        }

    }
}
