using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _db;
        public ContactUsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Bios.FirstOrDefault());
        }
    }
}
