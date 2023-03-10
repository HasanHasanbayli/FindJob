using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.DAL;
using Recruitment.Models;
using Recruitment.ViewModels;

namespace Recruitment.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(AppDbContext db, UserManager<AppUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.City = _db.Cities.ToList();
        HomeVM homeVM = new()
        {
            Bio = _db.Bios.FirstOrDefault(),
            PopularJobs = _db.PopularJobs,
            Statistics = _db.Statistics,
            Partners = _db.Partners,
            Blogs = _db.Blogs,
            PostJobs = _db.PostJobs.Where(x => x.IsActivated == true).Include(x => x.AppUser)
                .Include(x => x.AppUserPostJobs),
            CompaniesCount = _userManager.Users.Where(x => x.IsCompany == true).Count(),
            PostJobCount = _db.PostJobs.Count(),
            MembersCount = _userManager.Users.Where(x => x.IsCompany == false).Count(),
            ApplyCount = _db.AppUserPostJobs.Count(x => x.IsContacted == true)
        };
        if (User.Identity.IsAuthenticated)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            homeVM.AppUser = user;
        }

        return View(homeVM);
    }

    [Authorize]
    public async Task<IActionResult> AddToFavorite(int? id)
    {
        PostJob postJob = _db.PostJobs.Include(x => x.AppUserPostJobs).FirstOrDefault(x => x.Id == id);
        AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
        ViewBag.activeUser = user;
        if (user.AppUserPostJobs == null)
        {
            AppUserPostJob appUserPost = new()
            {
                AppUserId = postJob.AppUserId,
                AppendUserId = user.Id,
                IsFavorite = true,
                PostJobId = postJob.Id
            };
            _db.AppUserPostJobs.Add(appUserPost);
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    public async Task<IActionResult> RemoveFavorite(int? id, AppUserPostJob appUserPostJob)
    {
        if (id == null) return NotFound();
        AppUserPostJob dbPostjob = await _db.AppUserPostJobs.FindAsync(id);
        if (dbPostjob == null) return NotFound();
        _db.AppUserPostJobs.Remove(dbPostjob);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    public async Task<IActionResult> AddApply(int? id)
    {
        PostJob postJob = _db.PostJobs.Include(x => x.AppUserPostJobs).FirstOrDefault(x => x.Id == id);
        AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
        ViewBag.activeUser = user;
        if (user.AppUserPostJobs == null)
        {
            AppUserPostJob appUserPost = new()
            {
                AppUserId = postJob.AppUserId,
                AppendUserId = user.Id,
                PostJobId = postJob.Id,
                IsContacted = true
            };
            _db.AppUserPostJobs.Add(appUserPost);
        }

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    public async Task<IActionResult> RemoveApply(int? id, AppUserPostJob appUserPostJob)
    {
        if (id == null) return NotFound();
        AppUserPostJob dbPostjob = await _db.AppUserPostJobs.FindAsync(id);
        if (dbPostjob == null) return NotFound();
        _db.AppUserPostJobs.Remove(dbPostjob);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}