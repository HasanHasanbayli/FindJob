using System.ComponentModel.DataAnnotations;

namespace Recruitment.Models;

public class Subscription
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}