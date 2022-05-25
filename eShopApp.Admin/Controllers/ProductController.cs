using eShopApp.Admin.Models;
using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
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

       
        public IActionResult Add()
        {
            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductViewModel model)
        {
            if (model.Id == 0)
            {
                if (ModelState.ContainsKey("Id"))
                    ModelState.Remove("Id");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

              
                return View(model);
            }

            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();


            Product entity = new Product()
            {
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                Discontinued = model.Discontinued,
                CreatedDate = DateTime.Now,
                CategoryId = model.CategoryId,
                IsActive = true,
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
                TempData["Message"] = "Zorunlu alan.";
            }

            #endregion

            bool result;
            int currentUserId = GetCurrentUserId();

            entity.CreatedById = currentUserId;
            entity.CreatedDate = DateTime.Now;
            result = _productRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.Get(x => x.Id == id && x.IsActive, include: x => x.Include(y => y.Category));

            if (product != null)
            {
                var vm = new ProductViewModel()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    IsActive = product.IsActive,
                    UnitsInStock = product.UnitsInStock,
                    UnitPrice = product.UnitPrice,
                    Discontinued = product.Discontinued,
                };
                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

               
                return View(vm);
            }

            TempData["Message"] = "Ürün bulunamadı.";
            return View("List");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

                return View(model);
            }

            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();


            int currentUserId = GetCurrentUserId();
            var entity = new Product()
            {
                Id = model.Id,
                Discontinued = model.Discontinued,
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                UpdatedById = currentUserId,
                UpdatedDate = DateTime.Now,
                IsActive = model.IsActive,

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
                ViewBag.Message = "Boş dosya yükleyemezsiniz";
            }

            #endregion


            bool result;


            entity.Id = model.Id;
            entity.UpdatedById = currentUserId;
            result = _productRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "İşlem Gerçekleşemedi";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _productRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı" : "Silme işlemi gerçekleştirilemedi.";
            return RedirectToAction("List");
        }
    }
}
