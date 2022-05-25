using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
