using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Recruitment.Models;

public class Blog : SearchBase
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FontDescription { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }
}