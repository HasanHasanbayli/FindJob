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
    public class BioController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BioController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            Bio bio = _db.Bios.FirstOrDefault();
            ViewBag.Bio = bio;
            return View(_db.Bios.FirstOrDefault());            
        }

        public IActionResult Update()
        {
            Bio bio = _db.Bios.FirstOrDefault();
             if (bio == null) return NotFound();
            return View(bio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Bio bio)
        {
            //if (id == null) return NotFound();
            Bio dbBio = _db.Bios.FirstOrDefault();
            if (dbBio == null) return NotFound();
            if (bio.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!bio.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (bio.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mb ola biler");
                    return View();
                }
                string path = Path.Combine("assets", "images");
                if (path != null)
                {
                    Helper.DeleteImage(_env.WebRootPath, path, dbBio.Logo);
                }
                string fileName = await bio.Photo.SaveImg(_env.WebRootPath, path);
                dbBio.Logo = fileName;
                           }
            dbBio.Mobile = bio.Mobile;
            dbBio.Mobile2 = bio.Mobile2;
            dbBio.Fax = bio.Fax;
            dbBio.Adress = bio.Adress;
            dbBio.Facebook = bio.Facebook;
            dbBio.Twitter = bio.Twitter;
            dbBio.Youtube = bio.Youtube;
            dbBio.Linkedin = bio.Linkedin;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
