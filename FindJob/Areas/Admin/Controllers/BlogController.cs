using System;
using System.IO;
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
public class BlogController : Controller
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public BlogController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    public IActionResult Index(int? page)
    {
        ViewBag.PageCount = Math.Ceiling((decimal) _db.Blogs.Count() / 5);
        ViewBag.Page = page;
        if (page == null)
            return View(_db.Blogs.OrderByDescending(p => p.Id).Take(6).ToList());
        return View(_db.Blogs.OrderByDescending(p => p.Id).Skip(((int) page - 1) * 6).Take(6).ToList());
        return View(_db.Blogs.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog)
    {
        if (!ModelState.IsValid) return View();

        #region Photo

        if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
        if (!blog.Photo.IsImage())
        {
            ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
            return View();
        }

        if (blog.Photo.MaxLength(2000))
        {
            ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
            return View();
        }

        string path = Path.Combine("assets", "images", "Blog");
        string fileName = await blog.Photo.SaveImg(_env.WebRootPath, path);

        #endregion

        Blog newBlog = new();
        newBlog.Image = fileName;
        newBlog.Title = blog.Title;
        newBlog.Description = blog.Description;
        newBlog.FontDescription = blog.FontDescription;
        await _db.Blogs.AddAsync(newBlog);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int? id)
    {
        if (id == null) return NotFound();
        Blog blog = _db.Blogs.FirstOrDefault(x => x.Id == id);
        if (blog == null) return NotFound();
        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int? id, Blog blog)
    {
        if (id == null) return NotFound();
        Blog dbblog = _db.Blogs.FirstOrDefault(x => x.Id == id);
        if (dbblog == null) return NotFound();

        #region Photo

        if (blog.Photo != null)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }

            if (blog.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }

            string path = Path.Combine("assets", "images", "Blog");
            Helper.DeleteImage(_env.WebRootPath, path, dbblog.Image);
            string fileName = await blog.Photo.SaveImg(_env.WebRootPath, path);
            dbblog.Image = fileName;
        }

        #endregion

        dbblog.Title = blog.Title;
        dbblog.Description = blog.Description;
        dbblog.FontDescription = blog.FontDescription;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        Blog blog = _db.Blogs.FirstOrDefault(x => x.Id == id);
        if (blog == null) return NotFound();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null) return NotFound();
        Blog blog = _db.Blogs.FirstOrDefault(x => x.Id == id);
        if (blog == null) return NotFound();
        _db.Blogs.Remove(blog);
        string path = Path.Combine("assets", "images", "PopularJobs");
        Helper.DeleteImage(_env.WebRootPath, path, blog.Image);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}