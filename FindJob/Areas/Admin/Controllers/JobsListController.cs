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

        //public IActionResult Index()
        //{
        //    return View(_db.PostJobs.Include(x=>x.AppUser).ToList());
        //}

        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error404");
            PostJob postJob = _db.PostJobs.FirstOrDefault(x => x.Id == id);
            if(postJob==null) return RedirectToAction("Index", "Error404");
            return View(postJob);
        }
    }
}
