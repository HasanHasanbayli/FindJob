using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Bio=_db.Bios.FirstOrDefault(),
                PopularJobCategories=_db.PopularJobCategories,
                Statistics=_db.Statistics,
                Partners=_db.Partners,
                Blogs=_db.Blogs
            };
            return View(homeVM);
        }
    }
}
