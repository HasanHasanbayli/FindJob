using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.Models;
using FindJob.Services;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager, 
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email or Password is wrong!!");
                return View(login);
            }
            if (!appUser.IsActivated)
            {
                ModelState.AddModelError("", "Email is Disabled!!");
                return View(login);
            }
            var signinResult = await _signInManager.PasswordSignInAsync(appUser, login.Password, true, true);
            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong!!");
                return View(login);
            }
            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "The account is locked Out");
                return View(login);
            }
            //var role = (await _userManager.GetRolesAsync(appUser))[0];
            //if (role == "Admin")
            //{
            //    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            //}
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EmployeRegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email,
                IsActivated = true,
                IsCompany=false,
                CreateTime = DateTime.Now
        };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(newUser, "Employe");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var href = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = code }, protocol: Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(newUser.Email,
            "Confirm your Account", $"Qeydiyyati tamamlamaq ucun linkden kecid edin <a href='{href}'>click link</a>");
            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EmployerRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployerRegister(EmployerRegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                FullName = register.FullName,
                CompanyName = register.CompanyName,
                UserName = register.UserName,
                Email = register.Email,
                IsActivated = true,
                IsCompany = true,
                CreateTime = DateTime.Now
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            await _userManager.AddToRoleAsync(newUser, "Employer");
            await _signInManager.SignInAsync(newUser, true);
            return Redirect(Url.Action("Index", "Home"));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        //public async Task CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("Moderator"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("Employer"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Employer" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("Employe"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Employe" });
        //    }
        //}
    }
}