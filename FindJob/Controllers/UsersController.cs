 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Extentions;
using FindJob.Helpers;
using FindJob.Models;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(AppDbContext db, UserManager<AppUser> userManager, IWebHostEnvironment env, SignInManager<AppUser> signInManager) 
        {
            _env = env;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult JobList()
        {
            return View();
        }
        
        public IActionResult StaredJobs()
        {
            return View();
        }
        public async Task<IActionResult> UpdateProfile()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return View();
            return  View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateVM update)
        {
            if (!ModelState.IsValid) return View();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                AppUser existEmail = await _userManager.FindByEmailAsync(update.Email);
                AppUser existUserName = await _userManager.FindByNameAsync(update.UserName);
                if (existEmail != null || existUserName != null)
                {
                    ModelState.AddModelError("", "Email or UserName already taken!!");
                    return View(existEmail);
                }
                #region Photo
                if (update.Photo != null)
                {
                    if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        return View();
                    }
                    if (!update.Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                        return View();
                    }
                    if (update.Photo.MaxLength(2000))
                    {
                        ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                        return View();
                    }
                    string path = Path.Combine("assets","images", "Users");
                    if (user.Image!=null)
                    {
                        Helper.DeleteImage(_env.WebRootPath, path, user.Image);
                    }
                    string fileName = await update.Photo.SaveImg(_env.WebRootPath, path);
                    user.Image = fileName;
                }
                #endregion
                user.FullName = update.FullName;
                user.NormalizedEmail = update.Email;
                user.Email = update.Email;
                user.UserName = update.UserName;
                user.NormalizedUserName = update.UserName; 
                user.Location = update.Location;
                user.ExpectedSalary = update.ExpectedSalary;
                user.TotalExperience = update.TotalExperience;
                user.Skills = update.Skills;
                user.Description = update.Description;
                user.AboutCompanyDescription = update.AboutCompanyDescription;
                user.JobType = update.JobType;
            }
            await _db.SaveChangesAsync();
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult PostJob()
        {
            return View();
        }
    }
}
