using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FindJob.Models;

public class AppUser : IdentityUser
{
    [Required] [MaxLength(20)] public string FullName { get; set; }

    public string CompanyName { get; set; }
    public int Age { get; set; }
    public bool IsActivated { get; set; }
    public string Location { get; set; }
    public string JobType { get; set; }
    public string JobTitle { get; set; }
    public string ExpectedSalary { get; set; }
    public string TotalExperience { get; set; }
    public bool IsCompany { get; set; }
    public string Skills { get; set; }
    public string Description { get; set; }
    public string AboutCompanyDescription { get; set; }
    public DateTime CreateTime { get; set; }
    public string Image { get; set; }

    [NotMapped] public IFormFile Photo { get; set; }

    public string UserResume { get; set; }

    [NotMapped] public IFormFile Resume { get; set; }

    public ICollection<AppUserPostJob> AppUserPostJobs { get; set; }
    public ICollection<PostJob> PostJobs { get; set; }
}