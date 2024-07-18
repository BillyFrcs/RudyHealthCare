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
        public int PatientID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? QueueNumber { get; set; }

        public string? Name { get; set; }
        public string? IdentityNumber { get; set; }
        public string? PlaceOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOfRegistration { get; set; }

        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string? Status { get; set; }
        public string? Profession { get; set; }
        public string? QueueStatus { get; set; }
        public string? ComplaintsOfPain { get; set; }
        public string? DiagnoseResult { get; set; }
    }
}