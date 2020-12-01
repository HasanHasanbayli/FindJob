﻿ using System;
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
using Microsoft.EntityFrameworkCore;

namespace FindJob.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(AppDbContext db, 
                UserManager<AppUser> userManager, 
                IWebHostEnvironment env,
                SignInManager<AppUser> signInManager) 
        {
            _env = env;
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            PostJobVM postJobVM = new PostJobVM
            {
                AppUserPostJobs = _db.AppUserPostJobs.Include(x => x.AppUser).Include(x => x.PostJob)
            };
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                postJobVM.AppUser = user;
            }
            return View(postJobVM);
        }

        public async Task<IActionResult> StaredJobs()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var appUserPostJobs = _db.AppUserPostJobs.Include(x => x.PostJob).Where(x => x.AppUserId == user.Id);
            return View(appUserPostJobs);
        }
        //public async Task<IActionResult> Applied(int? id)
        //{
        //    PostJob postJob = await _db.PostJobs.FindAsync(id);
        //    postJob.IsFavorite=true;
        //    return View();
        //}

        public async Task<IActionResult> UpdateProfile()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return RedirectToAction("Index", "Error404");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateVM update)
        {
            if (!ModelState.IsValid) return View();
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                #region Unique email
                //AppUser existEmail = await _userManager.FindByEmailAsync(update.Email);
                //AppUser existUserName = await _userManager.FindByNameAsync(update.UserName);
                //if (existEmail != null || existUserName != null)
                //{
                //    ModelState.AddModelError("", "Email or UserName already taken!!");
                //    return View(existEmail);
                //}
                #endregion
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
                    string path = Path.Combine("assets", "images", "Users");
                    if (user.Image != null)
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
                user.Age = update.Age;
                user.CompanyName = update.CompanyName;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostJob(PostJob post)
        {
            //PostJob postJob =  _db.PostJobs.Include(x=>x./*AppUserPostJobs*/).ThenInclude(x=>x.AppUserId).FirstOrDefault(x=>x.Id==post.Id);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var appUserPostJob = postJob.AppUserPostJobs.Where(x => x.AppUserId == user.Id);
            if (!ModelState.IsValid) return View();
            PostJob newPost = new PostJob();
            newPost.JobDescription = post.JobDescription;
            newPost.JobTitle = post.JobTitle;
            newPost.RequiredExperience = post.RequiredExperience;
            newPost.Location = post.Location;
            newPost.Salary = post.Salary;
            newPost.CreateTime = DateTime.Now;
            newPost.ExpiresDate = post.ExpiresDate;
            newPost.JobType = post.JobType;
            newPost.Vacancies = post.Vacancies;
            newPost.Image = user.Image;
            newPost.Skills = post.Skills;
            newPost.AppUserId = user.Id;
            //List<AppUserPostJob> appUserPostJob2 = new List<AppUserPostJob>();
            //foreach (var item in post.AppUserPostJobs)
            //{
            //    appUserPostJob2.Add(item);

            //}
            //newPost.AppUserPostJobs = appUserPostJob2;
            await _db.PostJobs.AddAsync(newPost);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditJob(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob job = await _db.PostJobs.FindAsync(id);
            if (job == null) return RedirectToAction("Index", "Error404");
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJob(int? id, PostJob post)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return View();
            PostJob dbPost = await _db.PostJobs.FindAsync(id);
            dbPost.JobDescription = post.JobDescription;
            dbPost.JobTitle = post.JobTitle;
            dbPost.CompanyName = post.CompanyName;
            dbPost.RequiredExperience = post.RequiredExperience;
            dbPost.Location = post.Location;
            dbPost.Salary = post.Salary;
            dbPost.JobType = post.JobType;
            dbPost.Skills = post.Skills;
            dbPost.AppUserId = user.Id;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyJobList()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(_db.PostJobs.Include(x=>x.AppUserPostJobs).Where(h => h.AppUserId == user.Id).ToList());
        }

        public async Task<IActionResult> DeleteJob(int? id, PostJob job)
        {
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob dbJob = await _db.PostJobs.FindAsync(id);
            if (dbJob == null) return RedirectToAction("Index", "Error404");
            _db.PostJobs.Remove(dbJob);
            await _db.SaveChangesAsync();
            return RedirectToAction("MyJobList", "Users");
        }

        public async Task<IActionResult> BrowseJobs()
        {
            PostJobVM postJobVM = new PostJobVM
            {
                PostJobs = _db.PostJobs.Where(x => x.IsActivated == true).Include(x => x.AppUser).Include(x => x.AppUserPostJobs).ThenInclude(x => x.AppUser)
            };
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                postJobVM.AppUser = user;
            }
            return View(postJobVM);
        }

        public IActionResult JobDetail(int? id)
        {
            return View(_db.PostJobs.Include(p => p.AppUser).FirstOrDefault(x => x.Id == id));
        }

        public IActionResult FindStaff(AppUser user)
        {
            return View(_db.Users.Where(x => x.IsCompany == false).ToList());
        }

        public IActionResult UserProfile(string id)
        {
            var user = _userManager.Users.Include(p => p.PostJobs).Where(x => x.IsCompany == false).FirstOrDefault(z => z.Id == id);
            return View(user);
        }

        public IActionResult CompanyProfile(string id)
        {
            var user = _userManager.Users.Where(x => x.IsCompany == true).FirstOrDefault(z => z.Id == id);
            return View(user);
        }

        public async Task<IActionResult> Active(int? id, bool IsActivated)
        {
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob job = await _db.PostJobs.FindAsync(id);
            if (job == null) return RedirectToAction("Index", "Error404");
            job.IsActivated = IsActivated;
            await _db.SaveChangesAsync();
            return RedirectToAction("MyJobList", "Users");
        }

        public async Task<IActionResult> Deactive(int? id, bool IsActivated)
        {
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob job = await _db.PostJobs.FindAsync(id);
            if (job == null) return RedirectToAction("Index", "Error404");
            job.IsActivated = !IsActivated;
            await _db.SaveChangesAsync();
            return RedirectToAction("MyJobList", "Users");
        }
    }
}