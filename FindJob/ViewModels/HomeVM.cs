using FindJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.ViewModels
{
    public class HomeVM
    {
        public Bio Bio { get; set; }
        public IEnumerable<PopularJob> PopularJobs { get; set; }
        public IEnumerable<Statistics> Statistics { get; set; }
        public IEnumerable<Partners> Partners { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<PostJob> PostJobs { get; set; }
    }
}
