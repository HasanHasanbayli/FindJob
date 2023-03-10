using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FindJob.Models;

public class Partners
{
    public int Id { get; set; }
    public string Image { get; set; }

    [NotMapped] [Required] public IFormFile Photo { get; set; }
}