using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RudyHealthCare.Repositories.Patients;
using RudyHealthCare.Blueprints;
using RudyHealthCare.Models.Patients;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
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

        [HttpGet]
        [Route("/Admin/Queues/{queueStatus?}")]
        public async Task<IActionResult> Queues(string queueStatus = "Antri")
        {
            IEnumerable<PatientsModel> patientsQueueData = Enumerable.Empty<PatientsModel>();

            if (!string.IsNullOrEmpty(queueStatus))
            {
                patientsQueueData = await _repository.GetByQueueStatusAsync(queueStatus);
            }

            ViewData["Patients"] = patientsQueueData;

            return View("Views/Admin/Queues.cshtml");

            /*
            if (!string.IsNullOrEmpty(queueStatus))
            {
                var patientsQueueData = await _repository.GetByQueueStatusAsync(queueStatus);

                var patientsHelperBlueprint = new PatientsHelperBlueprint
                {
                    Patients = patientsQueueData
                };

                return View("Views/Admin/Queues.cshtml", patientsHelperBlueprint);
            }

            return View("Views/Admin/Queues.cshtml");
            */
        }

        [HttpGet]
        [Route("/Admin/MedicalRecords/{patientId?}")]
        public async Task<IActionResult> MedicalRecords(string patientId)
        {
            var patients = await _repository.GetByIdAsync(patientId);

            if (patients == null)
            {
                return NotFound();
            }

            var patientsMedicalRecordsBlueprint = new PatientsMedicalRecordsBlueprint
            {
                PatientId = patients.PatientId,
                QueueNumber = patients.QueueNumber,
                Name = patients.Name,
                IdentityNumber = patients.IdentityNumber,
                PlaceOfBirth = patients.PlaceOfBirth,
                DateOfBirth = patients.DateOfBirth,
                DateOfRegistration = patients.DateOfRegistration,
                TimeOfRegistration = patients.TimeOfRegistration,
                UpdatedAt = DateTime.Now,
                Age = patients.Age,
                Gender = patients.Gender,
                Address = patients.Address,
                Email = patients.Email,
                PhoneNumber = patients.PhoneNumber,
                Status = patients.Status,
                Profession = patients.Profession,
                QueueStatus = patients.QueueStatus,
                ComplaintsOfPain = patients.ComplaintsOfPain,
                DiagnoseResult = patients.DiagnoseResult
            };

            // ViewData["Patients"] = patients;

            return View("Views/Admin/MedicalRecords.cshtml", patientsMedicalRecordsBlueprint);
        }

        [HttpPost]
        [Route("/Admin/MedicalRecords/{patientId?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MedicalRecords(string patientId, PatientsMedicalRecordsBlueprint patientsMedicalRecordsBlueprint)
        {
            if (patientId != patientsMedicalRecordsBlueprint.PatientId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var patients = await _repository.GetByIdAsync(patientId);

                if (patients == null)
                {
                    return NotFound();
                }

                patients.PatientId = patientsMedicalRecordsBlueprint.PatientId;
                patients.QueueNumber = patientsMedicalRecordsBlueprint.QueueNumber;
                patients.Name = patientsMedicalRecordsBlueprint.Name;
                patients.IdentityNumber = patientsMedicalRecordsBlueprint.IdentityNumber;
                patients.PlaceOfBirth = patientsMedicalRecordsBlueprint.PlaceOfBirth;
                patients.DateOfBirth = patientsMedicalRecordsBlueprint.DateOfBirth;
                patients.DateOfRegistration = patientsMedicalRecordsBlueprint.DateOfRegistration;
                patients.TimeOfRegistration = patientsMedicalRecordsBlueprint.TimeOfRegistration;
                patients.UpdatedAt = DateTime.Now;
                patients.Age = patientsMedicalRecordsBlueprint.Age;
                patients.Gender = patientsMedicalRecordsBlueprint.Gender;
                patients.Address = patientsMedicalRecordsBlueprint.Address;
                patients.Email = patientsMedicalRecordsBlueprint.Email;
                patients.PhoneNumber = patientsMedicalRecordsBlueprint.PhoneNumber;
                patients.Status = patientsMedicalRecordsBlueprint.Status;
                patients.Profession = patientsMedicalRecordsBlueprint.Profession;
                patients.QueueStatus = patientsMedicalRecordsBlueprint.QueueStatus;
                patients.ComplaintsOfPain = patientsMedicalRecordsBlueprint.ComplaintsOfPain;
                patients.DiagnoseResult = patientsMedicalRecordsBlueprint.DiagnoseResult;

                try
                {
                    await _repository.UpdateAsync(patients);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PatientExists(patientsMedicalRecordsBlueprint.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Dashboard));
            }

            return View(nameof(Dashboard), patientsMedicalRecordsBlueprint);
        }

        private async Task<bool> PatientExists(string patientId)
        {
            return await _repository.GetByIdAsync(patientId) != null;
        }

        [HttpPost]
        [Route("/Admin/MedicalRecords/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var patients = await _repository.GetByIdAsync(id);

            if (patients != null)
            {
                await _repository.DeleteAsync(patients);
            }

            return RedirectToAction(nameof(Dashboard));

            // return Json(new { success = true });
        }

        [Route("/Admin/Profile")]
        public IActionResult Profile()
        {
            return View("Views/Admin/Profile.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}