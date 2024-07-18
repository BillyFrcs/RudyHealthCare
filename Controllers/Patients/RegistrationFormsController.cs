using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudyHealthCare.Models;

namespace RudyHealthCare.Controllers.Patients;

[Route("[controller]")]
public class RegistrationFormsController : Controller
{
    private readonly ILogger<RegistrationFormsController> _logger;

    public RegistrationFormsController(ILogger<RegistrationFormsController> logger)
    {
        _logger = logger;
    }

    [Route("/Patients/RegistrationForms")]
    public IActionResult RegistrationForms()
    {
        return View("Views/Patients/RegistrationForms.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}