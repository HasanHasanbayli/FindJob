using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class SeekersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JobList()
        {
            return View();
        }
        public IActionResult StaredJobs()
        {
            return View();
        }
        public IActionResult JobDetail()
        {
            return View();
        }
        public IActionResult UpdateProfile()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
