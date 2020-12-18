using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Extentions;
using FindJob.Helpers;
using FindJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin, Moderator"))]
    public class PopularJobController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public PopularJobController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_db.PopularJobs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PopularJob popularJob)
        {
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!popularJob.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (popularJob.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            if (_db.PopularJobs.Count() >= 8)
            {
                return RedirectToAction(nameof(Index));
            }
            string path = Path.Combine("assets","images", "PopularJobs");
            string fileName = await popularJob.Photo.SaveImg(_env.WebRootPath, path);
            PopularJob newJob = new PopularJob();
            newJob.Image = fileName;
            newJob.Title = popularJob.Title;
            newJob.Description = popularJob.Description;
            newJob.DataCount = popularJob.DataCount;
            _db.PopularJobs.Add(newJob);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            PopularJob popularJob = _db.PopularJobs.FirstOrDefault(x => x.Id == id);
            if (popularJob == null) return NotFound();
            return View(popularJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, PopularJob popularJob)
        {
            if (id == null) return NotFound();
            PopularJob dbPopularJob = _db.PopularJobs.FirstOrDefault(x => x.Id == id);
            if (popularJob == null) return NotFound();
            #region Photo
            if (popularJob.Photo!=null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }

                if (!popularJob.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }

                if (popularJob.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }

                string path = Path.Combine("assets", "images", "PopularJobs");
                Helper.DeleteImage(_env.WebRootPath, path, dbPopularJob.Image);
                string fileName = await popularJob.Photo.SaveImg(_env.WebRootPath, path);
                dbPopularJob.Image = fileName;
            }
            #endregion
            dbPopularJob.Title = popularJob.Title;
            dbPopularJob.Description = popularJob.Description;
            dbPopularJob.DataCount = popularJob.DataCount;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            PopularJob popularJob = _db.PopularJobs.FirstOrDefault(x => x.Id == id);
            if (popularJob == null) return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult>  DeletePost(int? id)
        {
            if (id == null) return NotFound();
            PopularJob popularJob = _db.PopularJobs.FirstOrDefault(x => x.Id == id);
            if (popularJob == null) return NotFound();
            _db.PopularJobs.Remove(popularJob);
            string path = Path.Combine("assets","images", "PopularJobs");
            Helper.DeleteImage(_env.WebRootPath, path, popularJob.Image);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
