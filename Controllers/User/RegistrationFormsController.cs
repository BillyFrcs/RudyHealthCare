using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudyHealthCare.Models;

namespace RudyHealthCare.Controllers.User;

public class RegistrationFormsController : Controller
{
    private readonly ILogger<RegistrationFormsController> _logger;

    public RegistrationFormsController(ILogger<RegistrationFormsController> logger)
    {
        _logger = logger;
    }

    [Route("/User/RegistrationForms")]
    public IActionResult RegistrationForms()
    {
        return View("Views/User/RegistrationForms.cshtml");
    }
}