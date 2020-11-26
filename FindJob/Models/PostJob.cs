﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Models
{
    public class PostJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string RequiredExperience { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string JobType { get; set; }
        public string Skills { get; set; }
        public string JobDescription{ get; set; }
        public bool IsActivated { get; set; }
        public bool IsFavorite { get; set; }
        public int Vacancies { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpiresDate { get; set; }
        public int Interest { get; set; }
        public int Contacted  { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
