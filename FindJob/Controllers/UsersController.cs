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
            #region
            //PostJobVM postJobVM = new PostJobVM
            //{
            //    AppUserPostJobs = _db.AppUserPostJobs.Include(x => x.AppUser).Include(x => x.PostJob)
            //};
            //if (User.Identity.IsAuthenticated)
            //{
            //    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //    postJobVM.AppUser = user;
            //}
            //var job = _db.PostJobs.Include(x=>x.).Where(x => x.AppUserId == user.Id).ToList();
            //return View(postJobVM);
            #endregion
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<AppUserPostJob> postJobs = _db.AppUserPostJobs.Where(x=>x.AppUserId==user.Id).ToList();
            List<AppUser> users = _userManager.Users.ToList();
            List<AppUser> StaredUsers = new List<AppUser>();
            List<AppUser> AppliedUsers = new List<AppUser>();

            foreach (var j in postJobs)
            {
                foreach (var u in users)
                {
                    if (u.Id == j.AppendUserId && j.IsFavorite==true)
                    {
                        StaredUsers.Add(u);
                    }
                }
            }
            foreach (var j in postJobs)
            {
                foreach (var u in users)
                {
                    if (u.Id == j.AppendUserId && j.IsContacted == true)
                    {
                        AppliedUsers.Add(u);
                    }
                }
            }
            PostJobVM postJobVM = new PostJobVM
            {
                AppUserPostJobs = _db.AppUserPostJobs.Include(x => x.AppUser).Where(x=>x.PostJob.AppUserId==user.Id).ToList(),
                StareddUser= StaredUsers,
                AppliedUser=AppliedUsers
            };
            postJobVM.AppUser = user;
            return View(postJobVM);
        }

        public async Task<IActionResult> StaredJobs()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var appUserPostJobs = _db.AppUserPostJobs.Include(x => x.PostJob).Where(x => x.AppendUserId == user.Id);
            return View(appUserPostJobs);
        }

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
                #region Resume
                if (update.Resume != null)
                {
                    if (ModelState["Resume"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        return View();
                    }
                    if (!update.Resume.ContentType.Contains("pdf"))
                    {
                        ModelState.AddModelError("Resume", "Zehmet olmasa pdf formati sechin");
                        return View();
                    }
                    if (update.Resume.MaxLength(3000))
                    {
                        ModelState.AddModelError("Resume", "Pdf olchusu max 3000kb ola biler");
                        return View();
                    }
                    string path = Path.Combine("assets", "pdf");
                    if (user.Resume != null)
                    {
                        Helper.DeleteImage(_env.WebRootPath, path, user.UserResume);
                    }
                    string fileName = await update.Resume.SaveImg(_env.WebRootPath, path);
                    user.UserResume = fileName;
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
        //[Authorize(Roles = ("Employer"))]
        public IActionResult PostJob()
        {
            ViewBag.City = _db.Cities.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostJob(PostJob post)
        {
            ViewBag.City = _db.Cities.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return View();
            PostJob newPost = new PostJob();
            newPost.JobDescription = post.JobDescription;
            newPost.JobTitle = post.JobTitle;
            newPost.CityId = post.CityId;
            newPost.JobCategoryId = post.JobCategoryId;
            newPost.RequiredExperience = post.RequiredExperience;
            newPost.Salary = post.Salary;
            newPost.CreateTime = DateTime.Now;
            newPost.ExpiresDate = post.ExpiresDate;
            newPost.JobType = post.JobType;
            newPost.Image = user.Image;
            newPost.Skills = post.Skills;
            newPost.AppUserId = user.Id;

            await _db.PostJobs.AddAsync(newPost);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditJob(int? id)
        {
            ViewBag.City = _db.Cities.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob job = await _db.PostJobs.FindAsync(id);
            if (job == null) return RedirectToAction("Index", "Error404");
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJob(int? id, PostJob post)
        {
            ViewBag.City = _db.Cities.ToList();
            ViewBag.JobCategory = _db.JobCategories.ToList();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return View();
            PostJob dbPost = await _db.PostJobs.FindAsync(id);
            dbPost.JobDescription = post.JobDescription;
            dbPost.JobTitle = post.JobTitle;
            dbPost.CityId = post.CityId;
            dbPost.JobCategoryId = post.JobCategoryId;
            dbPost.CompanyName = post.CompanyName;
            dbPost.RequiredExperience = post.RequiredExperience;
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

        public async Task<IActionResult> BrowseJobs(string category, string city, int? page)
        {
            PostJobVM postJobVM = new PostJobVM();
            
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                postJobVM.AppUser = user;
            }
            ViewBag.PageCount = Math.Ceiling((decimal)_db.PostJobs.Count() / 5);
            ViewBag.Page = page;
            if (page == null)
            {
               postJobVM.PostJobs = _db.PostJobs.Where(x => x.IsActivated == true)
                    .Include(x => x.City)
                    .Include(x => x.AppUser)
                    .Include(x => x.AppUserPostJobs)
                    .ThenInclude(x => x.AppUser)
                    .OrderByDescending(p => p.Id).Take(5).ToList();
            }
            else
            {
                postJobVM.PostJobs = _db.PostJobs.Where(x => x.IsActivated == true)
                    .Include(x => x.City)
                    .Include(x => x.AppUser)
                    .Include(x => x.AppUserPostJobs)
                    .ThenInclude(x => x.AppUser).OrderByDescending(p => p.Id).Skip(((int)page - 1) * 5).Take(5).ToList();
            }
            return View(postJobVM);
        }

        public IActionResult JobDetail(int? id)
        {
            ViewBag.City = _db.Cities.FirstOrDefault();
            return View(_db.PostJobs.Include(x=>x.City).Include(p => p.AppUser).FirstOrDefault(x => x.Id == id));
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

        //public IActionResult DownloadFile()
        //{
        //    string path = Path.Combine("assets", "pdf");
        //}

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