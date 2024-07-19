using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using RudyHealthCare.Models.Admin;

namespace RudyHealthCare.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Admin.Any())
                {
                    return;
                }

                context.Admin.AddRange(
                    new AdminModel
                    {
                        AdminId = Guid.NewGuid().ToString("N"),
                        Username = "billyfrcs",
                        Email = "billy@gmail.com",
                        Password = "billy1234",
                        Role = "Doctor"
                    },
                    new AdminModel
                    {
                        AdminId = Guid.NewGuid().ToString("N"),
                        Username = "satya",
                        Email = "satya@gmail.com",
                        Password = "satya1234",
                        Role = "Nurse"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}