using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using RudyHealthCare.Data;
using RudyHealthCare.Models;
using RudyHealthCare.Models.Patients;
using RudyHealthCare.Repositories.Patients;
using RudyHealthCare.ViewModels;

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

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }

    [Route("/Patients/RegistrationForms")]
    public IActionResult RegistrationForms()
    {
        return View("Views/Patients/RegistrationForms.cshtml");
    }

    [HttpPost]
    [Route("/Patients/RegistrationForms")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistrationForms(RegistrationFormsViewModel registrationFormsViewModel)
    {
        if (ModelState.IsValid)
        {
            var patients = new RegistrationFormsModel
            {
                PatientId = registrationFormsViewModel.PatientId,
                QueueNumber = registrationFormsViewModel.QueueNumber,
                Name = registrationFormsViewModel.Name,
                IdentityNumber = registrationFormsViewModel.IdentityNumber,
                PlaceOfBirth = registrationFormsViewModel.PlaceOfBirth,
                DateOfBirth = registrationFormsViewModel.DateOfBirth,
                DateOfRegistration = registrationFormsViewModel.DateOfRegistration,
                Age = registrationFormsViewModel.Age,
                Gender = registrationFormsViewModel.Gender,
                Address = registrationFormsViewModel.Address,
                Email = registrationFormsViewModel.Email,
                WhatsAppNumber = registrationFormsViewModel.WhatsAppNumber,
                Status = registrationFormsViewModel.Status,
                Profession = registrationFormsViewModel.Profession,
                QueueStatus = registrationFormsViewModel.QueueStatus,
                ComplaintsOfPain = registrationFormsViewModel.ComplaintsOfPain,
                DiagnoseResult = registrationFormsViewModel.DiagnoseResult
            };

            await _patientsRepository.AddAsync(patients);

            return RedirectToAction(nameof(Index));
        }

        return View(registrationFormsViewModel);
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