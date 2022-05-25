using eShopApp.Admin.Models;
using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRepository<Role> _roleRepository;
        public RoleController(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult List()
        {
            var roles = _roleRepository.GetAll(x => x.IsActive).Select(x => new RoleViewModel()
            {
                Id = x.Id,
                UserRole = x.UserRole,


            }).ToList();

            return View(roles);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Role()
            {
                IsActive = model.IsActive,
                CreatedById = currentUserId,
                UserRole = model.UserRole,
                CreatedDate = DateTime.Now,
            };

            var result = _roleRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Boş olmaz.";
            return View(model);
        }

        public IActionResult Edit(int id) 
        {
            var role = _roleRepository.Get(x => x.Id == id & x.IsActive);

            var currentUserId = GetCurrentUserId();

            if (role != null)
            {

                var vm = new RoleViewModel()
                {

                    Id = role.Id,
                    IsActive = role.IsActive

                };

                return View(vm);
            }

            TempData["Message"] = "Rol bulunamadı.";
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            Role entity = new Role()
            {
                Id = model.Id,
                IsActive = model.IsActive,
                UpdatedById = currentUserId,
                UpdatedDate = DateTime.Now,
                UserRole = model.UserRole,
            };

            bool result;

            result = _roleRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Boş dosya olmaz.";
            return View("Edit", model);

        }

        public IActionResult Delete(int id)
        {
            var result = _roleRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı" : "Silme işlemi gerçekleştirilemedi.";

            return RedirectToAction("List");
        }
    }
}
