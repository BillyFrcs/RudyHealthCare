using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudyHealthCare.Blueprints.Patients
{
    public class PatientsMedicalRecordsBlueprint
    {
        public string? PatientId { get; set; }

        [Required]
        public string? QueueNumber { get; set; }

        [Required(ErrorMessage = "Nama tidak boleh kosong")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Nomor KTP tidak boleh kosong")]
        [StringLength(16, ErrorMessage = "Nomor KTP tidak boleh lebih dari 16 karakter")]
        public string? IdentityNumber { get; set; }

        [Required(ErrorMessage = "Tempat lahir tidak boleh kosong")]
        public string? PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Tanggal lahir tidak boleh kosong")]
        public string? DateOfBirth { get; set; }

        [Required]
        public string? DateOfRegistration { get; set; }

        [Required]
        public string? TimeOfRegistration { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Umur tidak boleh kosong")]
        [Range(1, int.MaxValue, ErrorMessage = "Umur tidak boleh kurang dari 1")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Jenis kelamin tidak boleh kosong")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Nomor HP tidak boleh kosong")]
        [StringLength(14, ErrorMessage = "Nomor HP tidak boleh lebih dari 14 karakter")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Status tidak boleh kosong")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Pekerjaan tidak boleh kosong")]
        public string? Profession { get; set; }

        [Required]
        public string? QueueStatus { get; set; }

        [Required(ErrorMessage = "Keluhan penyakit tidak boleh kosong")]
        public string? ComplaintsOfPain { get; set; }

        [Required(ErrorMessage = "Hasil diagnosa tidak boleh kosong")]
        public string? DiagnoseResult { get; set; }

        [Required(ErrorMessage = "Rekomendasi obat tidak boleh kosong")]
        public string? MedicineRecommendations { get; set; }
    }
}