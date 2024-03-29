﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Helpers;
using FindJob.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FindJob.Extentions;

namespace FindJob.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = ("Admin, Moderator"))]
public class PartnerController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public PartnerController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public IActionResult Index()
    {
        return View(_db.Partners.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Partners partners)
    {
        if (!ModelState.IsValid) return View();

        #region Photo

        if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

        if (!partners.Photo.ContentType.Contains("image/svg+xml") && !partners.Photo.ContentType.Contains("image/"))
        {
            ModelState.AddModelError("Photo", "Zehmet olmasa image formati sechin");
            return View();
        }

        if (partners.Photo.MaxLength(2000))
        {
            ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
            return View();
        }

        string path = Path.Combine("assets", "images", "Partners");
        string fileName = await partners.Photo.SaveImg(_env.WebRootPath, path);

        #endregion

        Partners newPartner = new();
        newPartner.Image = fileName;
        await _db.Partners.AddAsync(newPartner);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int? id)
    {
        if (id == null) return NotFound();
        Partners partners = _db.Partners.FirstOrDefault(x => x.Id == id);
        if (partners == null) return NotFound();
        return View(partners);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int? id, PopularJob popularJob)
    {
        if (id == null) return NotFound();
        Partners dbPartners = _db.Partners.FirstOrDefault(x => x.Id == id);
        if (dbPartners == null) return NotFound();

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        Partners partners = _db.Partners.FirstOrDefault(x => x.Id == id);
        if (partners == null) return NotFound();
        return View(partners);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null) return NotFound();
        Partners partners = _db.Partners.FirstOrDefault(x => x.Id == id);
        if (partners == null) return NotFound();
        _db.Partners.Remove(partners);
        string path = Path.Combine("assets", "images", "PopularJobs");
        Helper.DeleteImage(_env.WebRootPath, path, partners.Image);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}