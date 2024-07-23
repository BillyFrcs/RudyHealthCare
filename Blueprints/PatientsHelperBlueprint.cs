using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RudyHealthCare.Models.Patients;

namespace RudyHealthCare.Blueprints
{
    public class PatientsHelperBlueprint
    {
        public IEnumerable<PatientsModel> Patients { get; set; } = [];

        public int TotalCount { get; set; }
        public int TotalCountQueue { get; set; }
        public int TotalCountAccepted { get; set; }
        public int TotalCountRejected { get; set; }

        [Required (ErrorMessage = "Search cannot be empty")]
        public string? SearchTerm { get; set; }
    }
}