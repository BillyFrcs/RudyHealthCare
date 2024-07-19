using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RudyHealthCare.ViewModels
{
    public class RegistrationFormsViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? IdentityNumber { get; set; }

        [Required]
        public string? PlaceOfBirth { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? Age { get; set; }

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
        public string? ComplaintsOfPain { get; set; }
        
        [Required]
        public string? PatientId 
        { 
            get => Guid.NewGuid().ToString("N");
        }

        [Required]
        public string? QueueNumber
        {
            get => "A001";
        }

        [Required]
        public string? QueueStatus 
        {
            get => "Antri";
        }

        [Required]
        public string? DiagnoseResult 
        {
            get => "Belum ada hasil diagnosa dari dokter.";
        }

        [Required]
        public DateTime DateOfRegistration 
        {
            get => DateTime.Now;
        }
    }
}