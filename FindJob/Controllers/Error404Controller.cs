using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers;

public class Error404Controller : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}