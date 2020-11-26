using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindJob.Controllers
{
    //public class StaredJobController : Controller
    //{
    //    private readonly AppDbContext _db;
    //    private readonly UserManager<AppUser> _userManager;
    //    public StaredJobController(AppDbContext db, UserManager<AppUser> userManager)
    //    {
    //        _db = db;
    //        _userManager = userManager;
    //    }
    //    [Authorize()]
    //    [Route("favorites")]
    //    //public async Task<IActionResult> Index()
    //    //{

    //    //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

    //    //    MyProfileVM model = new MyProfileVM
    //    //    {
    //    //        AppUser = user,
    //    //        UserStaredJobs = _db.UserStaredJobs.Where(x => x.AppUserId == user.Id).Include(f=>f.StaredJobs).ToList()
    //    //    };

    //    //    return View(model);
    //    //}
    //    #region Local Storage Version
    //    //public async Task<IActionResult> AddFavorite(int? id)
    //    //{
    //    //    if (id == null) return NotFound();
    //    //    PostJob job = await _db.PostJobs.FindAsync(id);
    //    //    if (job == null) return NotFound();
    //    //    List<FavoritesVM> jobs;
    //    //    string existFavorite = Request.Cookies["favorites"];
    //    //    if (existFavorite == null)
    //    //    {
    //    //        jobs = new List<FavoritesVM>();
    //    //    }
    //    //    else
    //    //    {
    //    //        jobs = JsonConvert.DeserializeObject<List<FavoritesVM>>(existFavorite);
    //    //    }
    //    //    FavoritesVM existFavorites = jobs.FirstOrDefault(z => z.Id == id);
    //    //    if (existFavorites == null)
    //    //    {
    //    //        FavoritesVM newFavorites = new FavoritesVM
    //    //        {
    //    //            Id = job.Id,
    //    //            Title = job.JobTitle,
    //    //            Createtime = job.CreateTime
    //    //        };
    //    //        jobs.Add(newFavorites);
    //    //    }
    //    //    else
    //    //    {
    //    //        List<FavoritesVM> favoritesVMs = new List<FavoritesVM>();
    //    //    }

    //    //    string favorites = JsonConvert.SerializeObject(jobs);
    //    //    Response.Cookies.Append("favorites", favorites, new CookieOptions { MaxAge = TimeSpan.FromDays(99) });
    //    //    return RedirectToAction("Index", "Home");
    //    //}
    //    #endregion
    //}
}
