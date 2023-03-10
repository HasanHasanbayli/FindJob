using System.Linq;
using System.Threading.Tasks;
using FindJob.DAL;
using FindJob.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindJob.ViewComponents;

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