using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RudyHealthCare.Controllers.Admin
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [Route("/Admin/Login")]
        public IActionResult Login()
        {
            return View("Views/Admin/Login.cshtml");
        }

        [Route("/Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View("Views/Admin/Dashboard.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}