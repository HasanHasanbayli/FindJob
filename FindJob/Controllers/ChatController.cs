using Microsoft.AspNetCore.Mvc;

namespace FindJob.Controllers;

public class ChatController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}