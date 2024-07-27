using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RudyHealthCare.Models.Patients
{
    public class PatientsModel
    {
        [Key]
        public string? PatientId { get; set; }

        [Required]
        public string? QueueNumber { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? IdentityNumber { get; set; }

        [Required]
        public string? PlaceOfBirth { get; set; }

        [Required]
        public string? DateOfBirth { get; set; }

        [Required]
        public string? DateOfRegistration { get; set; }

        [Required]
        public string? TimeOfRegistration { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public string? Profession { get; set; }

        [Required]
        public string? QueueStatus { get; set; }

        [Required]
        public string? ComplaintsOfPain { get; set; }

        [Required]
        public string? DiagnoseResult { get; set; }

        [Required]
        public string? MedicineRecommendations { get; set; }
    }
}