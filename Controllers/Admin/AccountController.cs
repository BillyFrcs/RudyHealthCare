using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RudyHealthCare.Blueprints.Admin;
using RudyHealthCare.Models.Admin;

namespace RudyHealthCare.Controllers.Admin
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("/Admin/Login")]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var admin = await _userManager.GetUserAsync(User);
                var roles = admin != null ? await _userManager.GetRolesAsync(admin) : null;

                if (roles != null && roles.Contains("Doctor"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (roles != null && roles.Contains("Nurse"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                // Redirect to a generic dashboard or home page if no specific role check is needed
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View("Views/Admin/Login.cshtml");
        }

        [HttpPost]
        [Route("/Admin/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminModel adminModel, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByEmailAsync(adminModel.Email);

                if (admin == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password");

                    return View("Views/Admin/Login.cshtml", adminModel);
                }

                var result = await _signInManager.PasswordSignInAsync(adminModel.Email, adminModel.Password, adminModel.RememberMe, lockoutOnFailure: false);

                // Console.WriteLine(result.Succeeded);

                if (result.Succeeded)
                {
                    var roles = admin != null ? await _userManager.GetRolesAsync(admin) : null;

                    Console.WriteLine(roles);

                    if (roles != null && roles.Contains("Doctor"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (roles != null && roles.Contains("Nurse"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password");

                    /*
                    if (result.IsLockedOut)
                    {
                        Console.WriteLine("This account has been locked out, please try again later.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        Console.WriteLine("You are not allowed to login.");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        Console.WriteLine("You need to provide two-factor authentication code.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid login attempt." + result.ToString());
                    }
                    */
                }

                /*
                var user = await _userManager.FindByEmailAsync(adminModel.Email);

                if (user == null)
                {
                    Console.WriteLine("Invalid login attempt. User not found.");
                }

                if (_userManager.Options.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(user))
                {
                    Console.WriteLine("You must confirm your email before logging in.");
                }
                else 
                {
                    Console.WriteLine("Email confirmed.");
                }

                var passwordValid = await _userManager.CheckPasswordAsync(user, adminModel.Password);

                if (!passwordValid)
                {
                    Console.WriteLine("Invalid login attempt. Incorrect password.");
                }
                else
                {
                    Console.WriteLine("Login successful.");
                }
                */
            }

            return View("Views/Admin/Login.cshtml", adminModel);

            /*
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByEmailAsync(adminModel.Email);

                if (admin != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(admin, adminModel.Password, adminModel.RememberMe, lockoutOnFailure: false);

                    // Console.WriteLine(result.Succeeded);

                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(admin);

                        Console.WriteLine(roles);

                        if (roles.Contains("Doctor"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else if (roles.Contains("Nurse"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                    }
                }
                else 
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            */
        }

        [Authorize]
        [HttpGet]
        [Route("/Admin/Profile")]
        public async Task<IActionResult> Profile()
        {
            var admin = await _userManager.GetUserAsync(User);
            var roles = admin != null ? await _userManager.GetRolesAsync(admin) : null;

            var adminAccountBlueprint = new AdminAccountBlueprint
            {
                Email = admin?.Email,
                Role = roles
            };

            return View("Views/Admin/Profile.cshtml", adminAccountBlueprint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}