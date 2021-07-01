using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recruitment.DAL;
using Recruitment.Models;
using Recruitment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Recruitment.Controllers
{
    public class StaredJobController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public StaredJobController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
