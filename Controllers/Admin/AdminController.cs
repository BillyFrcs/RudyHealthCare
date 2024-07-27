using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using RudyHealthCare.Repositories.Patients;
using RudyHealthCare.Blueprints;
using RudyHealthCare.Models.Patients;
using RudyHealthCare.Blueprints.Patients;

namespace RudyHealthCare.Controllers.Admin
{
    [Authorize]
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

        [HttpGet]
        [Route("/Admin/Dashboard")]
        public async Task<IActionResult> Dashboard(int page = 1, int pageSize = 10)
        {
            var totalCount = await _repository.GetTotalCountAsync();
            var totalCountQueue = await _repository.GetTotalCountQueueStatusAsync("Antri");
            var totalCountAccepted = await _repository.GetTotalCountQueueStatusAsync("Diterima");
            var totalCountRejected = await _repository.GetTotalCountQueueStatusAsync("Ditolak");
            var patientsData = await _repository.GetPatientsAsync(page, pageSize);

            // var patientsData = await _repository.GetAllAsync();

            var patientsHelperBlueprint = new PatientsHelperBlueprint
            {
                TotalCount = totalCount,
                TotalCountQueue = totalCountQueue,
                TotalCountAccepted = totalCountAccepted,
                TotalCountRejected = totalCountRejected,
                Patients = patientsData,
                PageNumber = page,
                PageSize = pageSize
            };

            if (patientsData != null)
            {
                return View("Views/Admin/Dashboard.cshtml", patientsHelperBlueprint);
            }

            return View("Views/Admin/Dashboard.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Search(PatientsHelperBlueprint patientsHelperBlueprint, int page = 1, int pageSize = 10)
        {
            var patientsData = await _repository.GetAllAsync(page, pageSize);

            /*
            if (!string.IsNullOrEmpty(patientsBlueprint.SearchTerm))
            {
                ModelState.AddModelError("", "Search term cannot be empty.");

                patientsBlueprint.Patients = Enumerable.Empty<PatientsModel>();
            }
            */

            if (!string.IsNullOrEmpty(patientsHelperBlueprint.SearchTerm))
            {
                var searchTermLower = patientsHelperBlueprint.SearchTerm.ToLower();

                patientsData = patientsData.Where(patient =>
                    patient.Name != null && patient.Name.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.IdentityNumber != null && patient.IdentityNumber.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.PhoneNumber != null && patient.PhoneNumber.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.Email != null && patient.Email.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.QueueNumber != null && patient.QueueNumber.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.QueueStatus != null && patient.QueueStatus.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.Gender != null && patient.Gender.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase) ||
                    patient.Profession != null && patient.Profession.Contains(searchTermLower, StringComparison.CurrentCultureIgnoreCase)
                );

                /*
                patientsData = patientsData.Where(patient =>
                    patient.Name.ToLower().Contains(searchTermLower) ||
                    patient.IdentityNumber.ToLower().Contains(searchTermLower) ||
                    patient.PhoneNumber.ToLower().Contains(searchTermLower) ||
                    patient.Email.ToLower().Contains(searchTermLower) ||
                    patient.QueueNumber.ToLower().Contains(searchTermLower) ||
                    patient.QueueStatus.ToLower().Contains(searchTermLower) ||
                    patient.Gender.ToLower().Contains(searchTermLower) ||
                    patient.Profession.ToLower().Contains(searchTermLower)
                );
                */
            }

            patientsHelperBlueprint.Patients = patientsData.ToList();
            patientsHelperBlueprint.PageNumber = page;
            patientsHelperBlueprint.PageSize = pageSize;
            patientsHelperBlueprint.SearchTerm = patientsHelperBlueprint.SearchTerm;

            return View(nameof(Dashboard), patientsHelperBlueprint);
        }

        [HttpGet]
        public async Task<IActionResult> SearchQueues(string searchTerm, int page = 1, int pageSize = 10)
        {
            var patientsData = await _repository.GetAllQueueStatusAsync(searchTerm, page, pageSize);

            var patientsHelperBlueprint = new PatientsHelperBlueprint
            {
                Patients = patientsData,
                PageNumber = page,
                PageSize = pageSize,
                SearchTerm = searchTerm
            };

            return View(nameof(Queues), patientsHelperBlueprint);
        }

        [HttpGet]
        [Route("/Admin/Queues/{queueStatus?}")]
        public async Task<IActionResult> Queues(string queueStatus = "Antri", int page = 1, int pageSize = 10)
        {
            if (!string.IsNullOrEmpty(queueStatus))
            {
                var patientsQueueData = await _repository.GetByQueueStatusAsync(queueStatus, page, pageSize);

                var patientsHelperBlueprint = new PatientsHelperBlueprint
                {
                    Patients = patientsQueueData,
                    PageNumber = page,
                    PageSize = pageSize
                };

                return View("Views/Admin/Queues.cshtml", patientsHelperBlueprint);
            }

            return View("Views/Admin/Queues.cshtml");

            /*
            IEnumerable<PatientsModel> patientsQueueData = Enumerable.Empty<PatientsModel>();

            if (!string.IsNullOrEmpty(queueStatus))
            {
                patientsQueueData = await _repository.GetByQueueStatusAsync(queueStatus);
            }

            ViewData["Patients"] = patientsQueueData;

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
                CreatedAt = patients.CreatedAt,
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
                DiagnoseResult = patients.DiagnoseResult,
                MedicineRecommendations = patients.MedicineRecommendations
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
                patients.CreatedAt = patientsMedicalRecordsBlueprint.CreatedAt;
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
                patients.MedicineRecommendations = patientsMedicalRecordsBlueprint.MedicineRecommendations;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}