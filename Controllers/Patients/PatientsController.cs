using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using RudyHealthCare.Blueprints;
using RudyHealthCare.Blueprints.Patients;
using RudyHealthCare.Data;
using RudyHealthCare.Models;
using RudyHealthCare.Models.Patients;
using RudyHealthCare.Repositories.Patients;
using RudyHealthCare.Services;

namespace RudyHealthCare.Controllers.Patients
{
    public class PatientsController : Controller
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly TwilioService _twilioService;
        private readonly EmailService _emailService;


        public PatientsController(IPatientsRepository patientsRepository, TwilioService twilioService, EmailService emailService)
        {
            _patientsRepository = patientsRepository;
            _twilioService = twilioService;
            _emailService = emailService;
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
                    TimeOfRegistration = patientsBlueprint.TimeOfRegistration,
                    CreatedAt = patientsBlueprint.CreatedAt,
                    Age = patientsBlueprint.Age,
                    Gender = patientsBlueprint.Gender,
                    Address = patientsBlueprint.Address,
                    Email = patientsBlueprint.Email,
                    PhoneNumber = patientsBlueprint.PhoneNumber,
                    Status = patientsBlueprint.Status,
                    Profession = patientsBlueprint.Profession,
                    QueueStatus = patientsBlueprint.QueueStatus,
                    ComplaintsOfPain = patientsBlueprint.ComplaintsOfPain,
                    DiagnoseResult = patientsBlueprint.DiagnoseResult,
                    MedicineRecommendations = patientsBlueprint.MedicineRecommendations
                };

                await _patientsRepository.AddAsync(patients);

                if (string.IsNullOrEmpty(patients.Email))
                {
                    Console.WriteLine("Invalid email." + patients.Email);
                }

                var message = $"Halo {patients.Name}, terima kasih telah registrasi di Rudy Health Care. Nomor antrian Anda adalah {patients.QueueNumber}. Silahkan menunggu panggilan dari resepsionis kami untuk konsultasi dengan dr. Rudy Gunawan.";

                if (!string.IsNullOrEmpty(patients.Email))
                {
                    await _emailService.SendEmailAsync(patients.Email, "Registrasi Berhasil", message);
                }

                /*
                // Send WhatsApp message
                if (string.IsNullOrEmpty(patients.PhoneNumber))
                {
                    Console.WriteLine("Invalid phone number." + patients.PhoneNumber);
                }

                string formatPhoneNumber = $"+{patients.PhoneNumber}";

                var message = $"Halo {patients.Name}, terima kasih telah mendaftar di Rudy Health Care. Nomor antrian Anda adalah {patients.QueueNumber}. Silahkan menunggu panggilan dari petugas kami.";

                await _twilioService.SendWhatsAppMessage(formatPhoneNumber, message);
                */

                return Json(new { success = true });

                // return RedirectToAction(nameof(Index));
            }

            // return View(registrationFormsViewModel);

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        public string GenerateQueueNumber(int number)
        {
            return $"A{number:D3}";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
};