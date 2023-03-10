using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DAL;
using Recruitment.Models;

namespace Recruitment.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    private readonly AppDbContext _db;

    public FooterViewComponent(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Bio model = _db.Bios.FirstOrDefault();
        return View(await Task.FromResult(model));
    }
}