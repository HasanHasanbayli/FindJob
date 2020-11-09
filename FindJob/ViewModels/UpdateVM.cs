using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.ViewModels
{
    public class UpdateVM
    {
        [StringLength(50)]
        public string FullName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        [StringLength(50)]
        public string JobType { get; set; }
        [StringLength(50)]
        public string ExpectedSalary { get; set; }
        [StringLength(50)]
        public string TotalExperience { get; set; }
        [StringLength(50)]
        public string Skills { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(200)]
        public string AboutCompanyDescription { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
