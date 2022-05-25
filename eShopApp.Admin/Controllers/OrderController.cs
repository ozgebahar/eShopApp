using eShopApp.Admin.Models;
using eShopApp.Data.Entities;
using eShopApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IRepository<Order> _orderRepository;

        private readonly IRepository<Product> _productRepository;

        public OrderController(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;

            _productRepository = productRepository;
        }

        public IActionResult List()
        {

            var orders = _orderRepository.GetAll().Select(x =>
                  new OrderViewModel()
                  {
                      Id = x.Id,
                      UserId = x.UserId,
                      OrderDate = x.OrderDate,
                      ShipAddress = x.ShipAddress,
                      User = x.User,
                      IsActive = x.IsActive
                  });


            return View(orders);
        }


        public ActionResult Edit(int id)
        {
            var orders = _orderRepository.Get(x => x.Id == id);

            if (orders != null)
            {
                var vm = new OrderViewModel()
                {
                    Id = orders.Id,
                    IsActive = orders.IsActive,
                    ShipAddress = orders.ShipAddress,
                    OrderDate = orders.OrderDate,
                    UserId = orders.UserId

                };

                return View(vm);
            }

            TempData["Message"] = "Sipariş bulunamadı.";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View("Edit", model);
            }

            var currentUserId = GetCurrentUserId();

            Order entity = new Order()
            {
                Id = model.Id,
                UserId = model.UserId,
                ShipAddress = model.ShipAddress,
                OrderDate = model.OrderDate,
                User = model.User,
                IsActive = model.IsActive
            };

            bool result;

            result = _orderRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "İşlem Gerkçekleşmedi";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _orderRepository.Delete(id);

            TempData["Message"] = result ? "Silme işlemi başarılı" : "Silme işlemi gerçekleştirilemedi.";

            return RedirectToAction("List");
        }
    }
}
