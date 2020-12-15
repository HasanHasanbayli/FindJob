using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public DashboardController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = "";
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.FullName = user.FullName;
                ViewBag.Image = user.Image;
            }
            return View();
        }
       
        public IActionResult Search(string search, string hidden)
        {
            List<AppUser> users = new List<AppUser>();
            IEnumerable<SearchBase> list = new List<SearchBase>();
            switch (hidden)
            {
                case "postjob":
                    list = _db.PostJobs.Where(t => t.JobTitle.ToLower().Contains(search.ToLower()));
                    return PartialView("_PostJobPartial", list);
                case "popularjob":
                    list = _db.PopularJobs.Where(b => b.Title.ToLower().Contains(search.ToLower()));
                    return PartialView("_PopularJobPartial", list);
                case "user":
                    users = _userManager.Users.Where(t => t.FullName.ToLower().Contains(search.ToLower())).ToList();
                    return PartialView("_UserPartial", users);
                default:
                    break;
            }
            return Ok(list);
        }
    }
}