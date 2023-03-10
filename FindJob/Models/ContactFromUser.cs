using System.ComponentModel.DataAnnotations;

namespace Recruitment.Models;

public class ContactFromUser : SearchBase
{
    public int Id { get; set; }

    [Required] public string FullName { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required] public string Subject { get; set; }

    [Required] public string Message { get; set; }

    public bool IsArchive { get; set; }
}