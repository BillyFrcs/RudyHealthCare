using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RudyHealthCare.Controllers.Admin
{
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

        [Route("/Admin/Queues")]
        public IActionResult Queues()
        {
            return View("Views/Admin/Queues.cshtml");
        }

        [Route("/Admin/Profile")]
        public IActionResult Profile()
        {
            return View("Views/Admin/Profile.cshtml");
        }

        [Route("/Admin/MedicalRecords")]
        public IActionResult MedicalRecords()
        {
            return View("Views/Admin/MedicalRecords.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}