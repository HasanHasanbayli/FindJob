﻿using System.ComponentModel.DataAnnotations;

namespace FindJob.ViewModels;

public class LoginVM
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}