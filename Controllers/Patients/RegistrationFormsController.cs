using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudyHealthCare.Blueprints;
using RudyHealthCare.Data;
using RudyHealthCare.Models;
using RudyHealthCare.Models.Patients;
using RudyHealthCare.Repositories.Patients;

namespace RudyHealthCare.Controllers.Patients;

public class RegistrationFormsController : Controller
{
    private readonly IPatientsRepository _patientsRepository;

    public RegistrationFormsController(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }

    /*
    private readonly ILogger<RegistrationFormsController> _logger;

    public RegistrationFormsController(ILogger<RegistrationFormsController> logger)
    {
        _logger = logger;
    }
    */

    /*
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
    public async Task<IActionResult> RegistrationForms(RegistrationFormsBlueprint registrationFormsBlueprint)
    {
        if (ModelState.IsValid)
        {
            var patients = new RegistrationFormsModel
            {
                PatientId = registrationFormsBlueprint.PatientId,
                QueueNumber = registrationFormsBlueprint.QueueNumber,
                Name = registrationFormsBlueprint.Name,
                IdentityNumber = registrationFormsBlueprint.IdentityNumber,
                PlaceOfBirth = registrationFormsBlueprint.PlaceOfBirth,
                DateOfBirth = registrationFormsBlueprint.DateOfBirth,
                DateOfRegistration = registrationFormsBlueprint.DateOfRegistration,
                Age = registrationFormsBlueprint.Age,
                Gender = registrationFormsBlueprint.Gender,
                Address = registrationFormsBlueprint.Address,
                Email = registrationFormsBlueprint.Email,
                WhatsAppNumber = registrationFormsBlueprint.WhatsAppNumber,
                Status = registrationFormsBlueprint.Status,
                Profession = registrationFormsBlueprint.Profession,
                QueueStatus = registrationFormsBlueprint.QueueStatus,
                ComplaintsOfPain = registrationFormsBlueprint.ComplaintsOfPain,
                DiagnoseResult = registrationFormsBlueprint.DiagnoseResult
            };

            await _patientsRepository.AddAsync(patients);

            return Json(new { success = true });

            // return RedirectToAction(nameof(Index));
        }

        // return View(registrationFormsViewModel);

        return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
    }

    /*
    [HttpGet]
    [Route("/Home")]
    public async Task<IActionResult> Index()
    {
        var patients = await _patientsRepository.GetAllAsync();

        return View(patients);
    }
    */

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}