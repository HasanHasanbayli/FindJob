using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindJob.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobsListController : Controller
    {
        private readonly AppDbContext _db;
        public JobsListController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.PostJobs.Include(x => x.City).Include(x=>x.AppUser).ToList());
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
}