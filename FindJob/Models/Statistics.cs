using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FindJob.Models;

public class Statistics
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Title { get; set; }
    public int DataCount { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }
}