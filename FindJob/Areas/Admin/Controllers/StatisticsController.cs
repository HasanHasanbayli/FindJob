using FindJob.DAL;
using FindJob.Extentions;
using FindJob.Helpers;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = ("Admin, Moderator"))]
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public StatisticsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_db.Statistics.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Statistics statistics)
        {
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!statistics.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (statistics.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            if (_db.Statistics.Count() >= 4)
            {
                return RedirectToAction(nameof(Index));
            }
            string path = Path.Combine("assets", "images", "Statistics");
            string fileName = await statistics.Photo.SaveImg(_env.WebRootPath, path);
            Statistics newStatus = new Statistics();
            newStatus.Image = fileName;
            newStatus.Title = statistics.Title;
            newStatus.DataCount = statistics.DataCount;
            _db.Statistics.Add(newStatus);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Statistics statistics = _db.Statistics.FirstOrDefault(x => x.Id == id);
            if (statistics == null) return NotFound();
            return View(statistics);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Statistics statistics)
        {
            if (id == null) return NotFound();
            Statistics dbStatistics = _db.Statistics.FirstOrDefault(x => x.Id == id);
            if (dbStatistics == null) return NotFound();
            #region Photo
            if (statistics.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!statistics.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (statistics.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }

                string path = Path.Combine("assets", "images", "Statistics");
                Helper.DeleteImage(_env.WebRootPath, path, dbStatistics.Image);
                string fileName = await statistics.Photo.SaveImg(_env.WebRootPath, path);
                dbStatistics.Image = fileName;
            }
            #endregion
            dbStatistics.Title = statistics.Title;
            dbStatistics.DataCount = statistics.DataCount;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Statistics statistics = _db.Statistics.FirstOrDefault(x => x.Id == id);
            if (statistics == null) return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null) return NotFound();
            Statistics statistics = _db.Statistics.FirstOrDefault(x => x.Id == id);
            if (statistics == null) return NotFound();
            _db.Statistics.Remove(statistics);
            string path = Path.Combine("assets", "images", "Statistics");
            Helper.DeleteImage(_env.WebRootPath, path, statistics.Image);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}