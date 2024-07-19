using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RudyHealthCare.Models.Admin
{
    public class AdminModel
    {
        [Key]
        public int AdminId { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}