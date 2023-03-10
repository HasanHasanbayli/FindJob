using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FindJob.Models;
using Microsoft.AspNetCore.Http;

namespace FindJob.ViewModels;

public class PopularJobVM
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string MyProperty { get; set; }
    public int DataCount { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }

    public IEnumerable<PopularJob> PopularJobs { get; set; }
}