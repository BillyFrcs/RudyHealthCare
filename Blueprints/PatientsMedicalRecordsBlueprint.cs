using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RudyHealthCare.Blueprints
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
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime DateOfRegistration { get; set; }

        [Required(ErrorMessage = "Umur tidak boleh kosong")]
        [Range(1, int.MaxValue, ErrorMessage = "Umur tidak boleh kurang dari 1")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Jenis kelamin tidak boleh kosong")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Nomor WhatsApp tidak boleh kosong")]
        [StringLength(12, ErrorMessage = "Nomor WhatsApp tidak boleh lebih dari 12 karakter")]
        public string? WhatsAppNumber { get; set; }

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
    }
}