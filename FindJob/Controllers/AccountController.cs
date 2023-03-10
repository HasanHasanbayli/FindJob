using System;
using System.Threading.Tasks;
using FindJob.Models;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FindJob.Controllers;

public class AccountController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

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

        SignInResult signinResult = await _signInManager.PasswordSignInAsync(appUser, login.Password, true, true);
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
        AppUser newUser = new()
        {
            FullName = register.FullName,
            UserName = register.UserName,
            Email = register.Email,
            IsActivated = true,
            IsCompany = false,
            CreateTime = DateTime.Now
        };
        IdentityResult identityResult = await _userManager.CreateAsync(newUser, register.Password);
        if (!identityResult.Succeeded)
        {
            foreach (IdentityError error in identityResult.Errors) ModelState.AddModelError("", error.Description);
            return View(register);
        }

        await _userManager.AddToRoleAsync(newUser, "Employe");
        ;
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
        AppUser newUser = new()
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
            foreach (IdentityError error in identityResult.Errors) ModelState.AddModelError("", error.Description);
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
        if (userId == null || code == null) return View("Error");
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user == null) return View("Error");
        IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");
        return View("Error");
    }

    public async Task CreateRole()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
        if (!await _roleManager.RoleExistsAsync("Moderator"))
            await _roleManager.CreateAsync(new IdentityRole {Name = "Moderator"});
        if (!await _roleManager.RoleExistsAsync("Employer"))
            await _roleManager.CreateAsync(new IdentityRole {Name = "Employer"});
        if (!await _roleManager.RoleExistsAsync("Employe"))
            await _roleManager.CreateAsync(new IdentityRole {Name = "Employe"});
    }
}