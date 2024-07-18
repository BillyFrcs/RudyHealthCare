using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using RudyHealthCare.Models.Patients;
using RudyHealthCare.Models.Admin;

namespace RudyHealthCare.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RegistrationFormsModel> Patients { get; set; }
        public DbSet<AdminModel> Admin { get; set; }
    }
}