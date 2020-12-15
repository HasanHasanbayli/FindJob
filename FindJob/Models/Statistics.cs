using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int DataCount { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
