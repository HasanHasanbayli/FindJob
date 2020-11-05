using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers
{
    public class EmployersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StaffList()
        {
            return View();
        }
        public IActionResult PostJob()
        {
            return View();
        }
        public IActionResult MyJobList()
        {
            return View();
        }
        public IActionResult UpdateProfile()
        {
            return View();
        }
        public IActionResult CompanyProfile()
        {
            return View();
        }
    }
}
