using eShopApp.Admin.Models;
using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
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
                PictureStr = Convert.ToBase64String(x.Picture),
                IsActive = x.IsActive
                
            }).ToList();

            return View(categories);
        }
        public IActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(CategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            Category entity = new Category()
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedById = currentUserId,
                IsActive=true
                
            };

            #region Picture için düzenleme
            if (model.Picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    var fileByteArray = ms.ToArray();
                    entity.Picture = fileByteArray;
                }
            }
            else
            {
                ViewBag.Message = "Boş dosya olmaz.";
            }
            #endregion

            var eklendiMi = _categoryRepository.Add(entity);

            if (eklendiMi)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.Get(x => x.Id == id);
            if (category != null)
            {
                var vm = new CategoryViewModel()
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    Description = category.Description,
                    IsActive = category.IsActive,
                    PictureStr = Convert.ToBase64String(category.Picture),

                };
                return View(vm);
            }
            return RedirectToAction("List"); ;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            Category entity = new Category()
            {
                Id = model.Id,
                CategoryName = model.CategoryName,
                Description = model.Description,
                UpdatedById = currentUserId,
                IsActive = model.IsActive,
                UpdatedDate = DateTime.Now,
            };

            #region Picture için düzenleme
            if (model.Picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.Picture.CopyTo(ms);
                    var fileByteArray = ms.ToArray();
                    entity.Picture = fileByteArray;
                }
            }
            else
            {
                ViewBag.Message = "Boş dosya olmaz.";
            }
            #endregion

            var eklendiMi = _categoryRepository.Edit(entity);

            if (eklendiMi)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _categoryRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı" : "Silme işlemi gerçekleştirilemedi.";

            return RedirectToAction("List");
        }
    }
}
