using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using RudyHealthCare.Models.Patients;
using RudyHealthCare.Models.Admin;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RudyHealthCare.Data
{
    // Default DbContext
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PatientsModel> Patients { get; set; }
        
        // public DbSet<AdminModel> Admin { get; set; }
    }
}