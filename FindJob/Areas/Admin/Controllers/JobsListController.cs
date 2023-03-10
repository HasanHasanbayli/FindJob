using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.DAL;
using Recruitment.Models;

namespace Recruitment.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = ("Admin, Moderator"))]
public class JobsListController : Controller
{
    private readonly AppDbContext _db;

    public JobsListController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(int? page)
    {
        ViewBag.PageCount = Math.Ceiling((decimal) _db.PostJobs.Count() / 5);
        ViewBag.Page = page;
        if (page == null)
            return View(_db.PostJobs.Include(x => x.City).Include(x => x.AppUser).OrderByDescending(p => p.Id).Take(6)
                .ToList());
        return View(_db.PostJobs.Include(x => x.City).Include(x => x.AppUser).OrderByDescending(p => p.Id)
            .Skip(((int) page - 1) * 6).Take(6).ToList());
        return View(_db.PostJobs.Include(x => x.City).Include(x => x.AppUser).ToList());
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        PostJob postJob = _db.PostJobs.FirstOrDefault(x => x.Id == id);
        if (postJob == null) return NotFound();
        return View(postJob);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null) return NotFound();
        PostJob postJob = _db.PostJobs.FirstOrDefault(x => x.Id == id);
        if (postJob == null) return NotFound();
        _db.PostJobs.Remove(postJob);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}