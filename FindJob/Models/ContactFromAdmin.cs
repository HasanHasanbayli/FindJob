using System.ComponentModel.DataAnnotations;

namespace Recruitment.Models;

public class ContactFromAdmin
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }

    [Required(ErrorMessage = "Please, add your message!")]
    public string Message { get; set; }
}