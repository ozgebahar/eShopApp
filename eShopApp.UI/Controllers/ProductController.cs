using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using eShopApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List(int? id)
        {
            var products = _productRepository.GetAll(include: x => x.Include(y => y.Category))
                .Where(x => x.IsActive);

            if (id != null)
            {
                products = _productRepository.GetAll(x => x.CategoryId == id);
            }

            var vm = products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                Discontinued = x.Discontinued,
                CategoryName = x.Category.CategoryName,
                PictureStr = Convert.ToBase64String(x.Picture)

            }).ToList();

            return View(vm);
        }
    }
}
