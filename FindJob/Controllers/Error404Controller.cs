﻿using Microsoft.AspNetCore.Mvc;

namespace Recruitment.Controllers;

public class Error404Controller : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}