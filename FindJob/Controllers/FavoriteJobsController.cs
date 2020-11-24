using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FindJob.Controllers
{
    public class FavoriteJobsController : Controller
    {
        private readonly AppDbContext _db;
        public FavoriteJobsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddFavorite(int? id)
        {
            if (id == null) return NotFound();
            PostJob job = await _db.PostJobs.FindAsync(id);
            if (job == null) return NotFound();
            List<FavoritesVM> jobs;
            string existFavorite = Request.Cookies["favorites"];
            if (existFavorite==null)
            {
                jobs = new List<FavoritesVM>();
            }
            else
            {
                jobs = JsonConvert.DeserializeObject<List<FavoritesVM>>(existFavorite);
            }
            FavoritesVM existFavorites = jobs.FirstOrDefault(z => z.Id == id);
            if (existFavorites==null)
            {
                FavoritesVM newFavorites = new FavoritesVM
                {
                    Id = job.Id,
                    Title = job.JobTitle,
                    Createtime = job.CreateTime
                };
                jobs.Add(newFavorites);
            }
            else
            {
                List<FavoritesVM> favoritesVMs = new List<FavoritesVM>();
            }
            
            string favorites = JsonConvert.SerializeObject(jobs);
            Response.Cookies.Append("favorites", favorites, new CookieOptions { MaxAge = TimeSpan.FromDays(99) });
            return RedirectToAction("Index", "Home");
        }
    }
}
