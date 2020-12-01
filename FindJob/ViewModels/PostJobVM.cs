using FindJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.ViewModels
{
    public class PostJobVM
    {
        public IEnumerable<PostJob> PostJobs { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<AppUserPostJob> AppUserPostJobs { get; set; }
    }
}
