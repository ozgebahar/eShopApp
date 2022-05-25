using eShopApp.Admin.Models;
using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IRepository<User> _userRepository;
        public AuthController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Password = model.Password,
                CreatedById = -1,
                CreatedDate = DateTime.Now
            };

            var result = _userRepository.Add(user);
            if (result)
            {
               
                TempData["Message"] = "Kayıt Başarılı";
                return RedirectToAction("Login");
            }

            TempData["Message"] = "Kayıt olunamadı.";
            return View(model);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var user = _userRepository.Get(x => x.Email == model.Email && x.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.Firstname),
                    new Claim(ClaimTypes.Surname,user.Lastname),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("index", "home");
            }

            ViewBag.Message = "Kullanıcı adınızı veya şifrenizi kontrol ediniz.";
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}

