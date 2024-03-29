﻿using System.Collections.Generic;
using System.Linq;
using FindJob.DAL;
using FindJob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers;

public class SearchController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<AppUser> _userManager;

    public SearchController(AppDbContext db, UserManager<AppUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Search(string search, string hidden)
    {
        var users = new List<AppUser>();
        IEnumerable<SearchBase> list = new List<SearchBase>();
        switch (hidden)
        {
            case "postjobfront":
                list = _db.PostJobs.Where(t => t.JobTitle.ToLower().Contains(search.ToLower())).Take(5);
                return PartialView("_PostJobPartialFront", list);
            case "userfront":
                users = _userManager.Users.Where(x => x.IsCompany == false)
                    .Where(t => t.UserName.ToLower().Contains(search.ToLower())).Take(5).ToList();
                return PartialView("_UserPartialFront", users);
            case "blogfront":
                list = _db.Blogs.Where(t => t.Title.ToLower().Contains(search.ToLower())).Take(5).ToList();
                return PartialView("_BlogPartialFront", list);
        }

        return Ok(list);
    }
}