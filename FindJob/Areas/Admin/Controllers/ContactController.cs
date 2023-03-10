using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DAL;
using Recruitment.Models;

namespace Recruitment.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = ("Admin, Moderator"))]
public class ContactController : Controller
{
    private readonly AppDbContext _db;

    public ContactController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(int? page)
    {
        ViewBag.PageCount = Math.Ceiling((decimal) _db.ContactFromUsers.Count() / 5);
        ViewBag.Page = page;
        if (page == null)
            return View(_db.ContactFromUsers.OrderByDescending(p => p.Id).Take(6).ToList());
        return View(_db.ContactFromUsers.OrderByDescending(p => p.Id).Skip(((int) page - 1) * 6).Take(6).ToList());
        return View(_db.ContactFromUsers.Where(x => x.IsArchive == false).ToList());
    }

    public IActionResult Detail(int? id)
    {
        if (id == null) return NotFound();
        ContactFromUser contact = _db.ContactFromUsers.FirstOrDefault(x => x.Id == id);
        if (contact == null) return NotFound();
        return View(contact);
    }

    public IActionResult SendEmail()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendEmail(int id, ContactFromAdmin sendMessage)
    {
        if (ModelState.IsValid)
        {
            string email = _db.ContactFromUsers.FirstOrDefault(m => m.Id == id).Email;
            sendMessage.Email = email;

            MailMessage message = new();
            message.From = new MailAddress("Jodice@mail.ru", "Jodice");
            message.To.Add(new MailAddress(sendMessage.Email));

            message.Subject = sendMessage.Subject;
            message.Body = sendMessage.Message;

            SmtpClient smtp = new();
            smtp.Host = "smtp.google.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("hasannh@code.edu.azww", "Jodice123");
            smtp.Send(message);

            return RedirectToAction(nameof(Index));
        }

        return View(sendMessage);
    }

    public async Task<IActionResult> Archive(int? id)
    {
        if (id == null) return NotFound();
        ContactFromUser contact = _db.ContactFromUsers.FirstOrDefault(x => x.Id == id);
        if (contact == null) return NotFound();
        contact.IsArchive = true;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ArchivedList()
    {
        return View(_db.ContactFromUsers.Where(a => a.IsArchive == true).ToList());
    }

    public async Task<IActionResult> Restore(int? id)
    {
        if (id == null) return NotFound();
        ContactFromUser contact = _db.ContactFromUsers.FirstOrDefault(x => x.Id == id);
        if (contact == null) return NotFound();
        contact.IsArchive = false;
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(ArchivedList));
    }

    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
        ContactFromUser contact = _db.ContactFromUsers.FirstOrDefault(x => x.Id == id);
        if (contact == null) return NotFound();
        return View(contact);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, ContactFromUser contact)
    {
        if (id == null) return NotFound();
        ContactFromUser dbContact = await _db.ContactFromUsers.FindAsync(id);
        if (dbContact == null) return NotFound();
        _db.ContactFromUsers.Remove(dbContact);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}