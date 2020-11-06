using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Models
{
    public class AppUser: IdentityUser
    {
        [Required, MaxLength(20)]
        public string FullName { get; set; }
        public bool IsActivated { get; set; }
    }
}
