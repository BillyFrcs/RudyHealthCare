using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using RudyHealthCare.Models.Admin;

namespace RudyHealthCare.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userManager = scopedServices.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync();

                await CreateRolesAndAdmin(userManager, roleManager);
            }
        }

        private static async Task CreateRolesAndAdmin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Doctor", "Nurse" };

            IdentityResult roleResult;

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var admins = new List<AdminModel>
            {
                new AdminModel
                {
                    Email = "billy@gmail.com",
                    Password = "Billy@1234",
                    Role = "Doctor",
                    RememberMe = false
                },
                new AdminModel
                {
                    Email = "satya@gmail.com",
                    Password = "Satya@1234",
                    Role = "Nurse",
                    RememberMe = false
                }
            };

            foreach (var admin in admins)
            {
                await CreateAdmins(userManager, admin);
            }
        }

        private static async Task CreateAdmins(UserManager<IdentityUser> userManager, AdminModel adminModel)
        {
            if (userManager.Users.All(admin => admin.Email != adminModel.Email))
            {
                if (string.IsNullOrEmpty(adminModel.Password) || string.IsNullOrEmpty(adminModel.Role))
                {
                    throw new ArgumentException("Password and Role are required");
                }

                var admin = new IdentityUser
                {
                    UserName = adminModel.Email,
                    Email = adminModel.Email,
                    EmailConfirmed = true,
                    PasswordHash = adminModel.Password,
                    LockoutEnabled = false
                };

                var createAdminResult = await userManager.CreateAsync(admin, adminModel.Password);

                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminModel.Role);
                }
            }
        }

        /*
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Doctor", "Nurse" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await EnsureUserAsync(userManager, "billy@gmail.com", "Billy@1234", "Doctor");
            await EnsureUserAsync(userManager, "satya@gmail.com", "Satya@1234", "Nurse");
        }

        private static async Task EnsureUserAsync(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true, LockoutEnabled = false };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    Console.WriteLine("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
        */
    }
}