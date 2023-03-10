using System.ComponentModel.DataAnnotations;

namespace FindJob.ViewModels;

public class EmployerRegisterVM
{
    [Required] [StringLength(50)] public string FullName { get; set; }

    [Required] [StringLength(50)] public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    [Required] [StringLength(50)] public string CompanyName { get; set; }
}