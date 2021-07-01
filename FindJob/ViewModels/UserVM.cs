using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string JobType { get; set; }
        public string JobTitle { get; set; }
        public string ExpectedSalary { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public bool IsActivated { get; set; }
        public List<string> Roles { get; set; }
        public bool IsArchived { get; set; }
        public bool IsCompany { get; set; }
    }
}
