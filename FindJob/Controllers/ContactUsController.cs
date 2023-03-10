using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DAL;
using Recruitment.Models;
using Recruitment.ViewModels;

namespace Recruitment.Controllers;

public class ContactUsController : Controller
{
    private readonly AppDbContext _db;

    public ContactUsController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        ContactVM contact = new()
        {
            Bio = _db.Bios.FirstOrDefault()
        };
        return View(contact);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactVM contact)
    {
        ContactVM contact2 = new()
        {
            Bio = _db.Bios.FirstOrDefault(),
            ContactFromUser = _db.ContactFromUsers.FirstOrDefault()
        };
        if (!ModelState.IsValid) return View(contact2);


        ContactFromUser newContact = new();
        newContact.FullName = contact.ContactFromUser.FullName;
        newContact.Email = contact.ContactFromUser.Email;
        newContact.Subject = contact.ContactFromUser.Subject;
        newContact.Message = contact.ContactFromUser.Message;
        newContact.PhoneNumber = contact.ContactFromUser.PhoneNumber;
        await _db.ContactFromUsers.AddAsync(newContact);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}