using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
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
            return View(_db.PostJobs.Include(x=>x.AppUser).ToList());
        }
    }
}
