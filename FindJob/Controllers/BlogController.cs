using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Blogs.ToList());
        }
        public IActionResult Detail(int? id)
        {
            return View(_db.Blogs.FirstOrDefault(x => x.Id == id));
        }
    }
}
