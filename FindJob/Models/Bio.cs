﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FindJob.Models;

public class Bio
{
    public int Id { get; set; }
    public string Logo { get; set; }
    public string Mobile { get; set; }
    public string Mobile2 { get; set; }
    public string Fax { get; set; }
    public string Adress { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string Linkedin { get; set; }
    public string Youtube { get; set; }

    [NotMapped] public IFormFile Photo { get; set; }
}