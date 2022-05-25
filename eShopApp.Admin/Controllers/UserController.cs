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
    public class UserController : BaseController
    {
        private readonly IRepository<User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult List()
        {
            var users = _userRepository.GetAll().Select(x =>
            new UserViewModel()
            {
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Id = x.Id,
                Password = x.Password
            }).ToList();

            return View(users);
        }
        public IActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(UserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            User entity = new User()
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Password = model.Password,
                IsActive=true
            };

           

            var eklendiMi = _userRepository.Add(entity);

            if (eklendiMi)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var user = _userRepository.Get(x => x.Id == id);
            if (user != null)
            {
                var vm = new UserViewModel()
                {

                    Id = user.Id,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Password = user.Password

                };
                return View(vm);
            }
            return RedirectToAction("List"); ;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            User entity = new User()
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Password = model.Password
            };

          

            var eklendiMi = _userRepository.Edit(entity);

            if (eklendiMi)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _userRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı" : "Silme işlemi gerçekleştirilemedi.";

            return RedirectToAction("List");
        }
    }
}

