using Microsoft.AspNetCore.Mvc;

namespace Recruitment.Controllers;

public class ChatController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}