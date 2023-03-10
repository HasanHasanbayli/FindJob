using System.ComponentModel.DataAnnotations;

namespace FindJob.Models;

public class Subscription
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}