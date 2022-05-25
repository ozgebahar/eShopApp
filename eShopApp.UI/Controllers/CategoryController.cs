using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using eShopApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.UI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
           
        }

        public IActionResult List()
        {
            var categories = _categoryRepository.GetAll().Select(x =>
            new CategoryViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                PictureStr = Convert.ToBase64String(x.Picture)
            }).ToList();

            return View(categories);
        }
    }
}
