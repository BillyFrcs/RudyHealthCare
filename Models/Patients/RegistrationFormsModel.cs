using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RudyHealthCare.Models.Patients
{
    public class RegistrationFormsModel
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
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfRegistration { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? WhatsAppNumber { get; set; }

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
    }
}