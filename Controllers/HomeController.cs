using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RudyHealthCare.Blueprints.Patients;
using RudyHealthCare.Models;
using RudyHealthCare.Repositories.Patients;

namespace RudyHealthCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientsRepository _repository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPatientsRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [Route("/Home")]
        public async Task<IActionResult> Index()
        {
            var totalCountQueue = await _repository.GetTotalCountQueueStatusAsync("Antri");

            var patientsHelperBlueprint = new PatientsHelperBlueprint
            {
                TotalCountQueue = totalCountQueue
            };

            return View(patientsHelperBlueprint);
        }

        [Route("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}