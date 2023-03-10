using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DAL;
using Recruitment.Models;

namespace Recruitment.Controllers;

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