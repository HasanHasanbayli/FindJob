﻿using System.Collections.Generic;

namespace FindJob.Models;

public class JobCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<PostJob> PostJobs { get; set; }
}