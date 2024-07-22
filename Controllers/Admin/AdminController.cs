using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RudyHealthCare.Repositories.Patients;
using RudyHealthCare.Blueprints;

namespace RudyHealthCare.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IPatientsRepository _repository;

        public AdminController(IPatientsRepository repository)
        {
            _repository = repository;
        }

        /*
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        */

        [Route("/Admin/Login")]
        public IActionResult Login()
        {
            return View("Views/Admin/Login.cshtml");
        }

        [Route("/Admin/Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var totalCount = await _repository.GetTotalCountAsync();
            var totalCountQueue = await _repository.GetTotalCountQueueStatusAsync("Antri");
            var totalCountAccepted = await _repository.GetTotalCountQueueStatusAsync("Diterima");
            var totalCountRejected = await _repository.GetTotalCountQueueStatusAsync("Ditolak");
            var patientsData = await _repository.GetAllAsync();

            var patientsHelperBlueprint = new PatientsHelperBlueprint
            {
                TotalCount = totalCount,
                TotalCountQueue = totalCountQueue,
                TotalCountAccepted = totalCountAccepted,
                TotalCountRejected = totalCountRejected,
                Patients = patientsData
            };

            if (patientsData != null)
            {
                return View("Views/Admin/Dashboard.cshtml", patientsHelperBlueprint);
            }

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