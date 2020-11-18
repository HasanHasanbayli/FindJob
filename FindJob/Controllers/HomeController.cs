using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Bio = _db.Bios.FirstOrDefault(),
                PopularJobCategories = _db.PopularJobCategories,
                Statistics = _db.Statistics,
                Partners = _db.Partners,
                Blogs = _db.Blogs,
                PostJobs = _db.PostJobs.Where(x=>x.IsActivated==true).Include(x => x.AppUser)
            };
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                homeVM.AppUser = user;
            }
            
            return View(homeVM);
        }

    }
}
