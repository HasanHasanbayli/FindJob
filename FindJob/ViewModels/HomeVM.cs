﻿using System.Collections.Generic;
using FindJob.Models;

namespace FindJob.ViewModels;

public class HomeVM
{
    public Bio Bio { get; set; }
    public IEnumerable<PopularJob> PopularJobs { get; set; }
    public IEnumerable<Statistics> Statistics { get; set; }
    public IEnumerable<Partners> Partners { get; set; }
    public IEnumerable<Blog> Blogs { get; set; }
    public AppUser AppUser { get; set; }
    public IEnumerable<PostJob> PostJobs { get; set; }
    public int CompaniesCount { get; set; }
    public int MembersCount { get; set; }
    public int PostJobCount { get; set; }
    public int ApplyCount { get; set; }
    public IEnumerable<JobCategory> JobCategory { get; set; }
}