using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudyHealthCare.Blueprints;
using RudyHealthCare.Data;
using RudyHealthCare.Models;
using RudyHealthCare.Models.Patients;
using RudyHealthCare.Repositories.Patients;

namespace RudyHealthCare.Controllers.Patients
{
    public class PatientsController : Controller
    {
        private readonly IPatientsRepository _patientsRepository;

        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        /*
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(ILogger<PatientsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        */

        [Route("/Patients/RegistrationForms")]
        public IActionResult RegistrationForms()
        {
            return View("Views/Patients/RegistrationForms.cshtml");
        }

        [HttpPost]
        [Route("/Patients/RegistrationForms")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrationForms(PatientsBlueprint patientsBlueprint)
        {
            if (ModelState.IsValid)
            {
                var patients = new PatientsModel
                {
                    PatientId = patientsBlueprint.PatientId,
                    QueueNumber = patientsBlueprint.QueueNumber,
                    Name = patientsBlueprint.Name,
                    IdentityNumber = patientsBlueprint.IdentityNumber,
                    PlaceOfBirth = patientsBlueprint.PlaceOfBirth,
                    DateOfBirth = patientsBlueprint.DateOfBirth,
                    DateOfRegistration = patientsBlueprint.DateOfRegistration,
                    Age = patientsBlueprint.Age,
                    Gender = patientsBlueprint.Gender,
                    Address = patientsBlueprint.Address,
                    Email = patientsBlueprint.Email,
                    WhatsAppNumber = patientsBlueprint.WhatsAppNumber,
                    Status = patientsBlueprint.Status,
                    Profession = patientsBlueprint.Profession,
                    QueueStatus = patientsBlueprint.QueueStatus,
                    ComplaintsOfPain = patientsBlueprint.ComplaintsOfPain,
                    DiagnoseResult = patientsBlueprint.DiagnoseResult
                };

                await _patientsRepository.AddAsync(patients);

                return Json(new { success = true });

                // return RedirectToAction(nameof(Index));
            }

            // return View(registrationFormsViewModel);

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
};