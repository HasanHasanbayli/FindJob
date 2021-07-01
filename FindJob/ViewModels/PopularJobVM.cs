using Recruitment.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.ViewModels
{
    public class PopularJobVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string MyProperty { get; set; }
        public int DataCount { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
        public IEnumerable<PopularJob> PopularJobs { get; set; }
    }
}
