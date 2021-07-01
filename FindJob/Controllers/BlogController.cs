using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recruitment.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Recruitment.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? page)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Blogs.Count() / 5);
            ViewBag.Page = page;
            if (page == null)
            {
                return View(_db.Blogs.OrderByDescending(p => p.Id).Take(3).ToList());
            }
            else
            {
                return View(_db.Blogs.OrderByDescending(p => p.Id).Skip(((int)page - 1) * 3).Take(3).ToList());

            }
            return View(_db.Blogs.ToList());
        }
        public IActionResult Detail(int? id)
        {
            return View(_db.Blogs.FirstOrDefault(x => x.Id == id));

        }
    }
}
