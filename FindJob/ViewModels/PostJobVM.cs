using Recruitment.Models;
using System.Collections.Generic;

namespace Recruitment.ViewModels
{
    public class PostJobVM
    {
        public IEnumerable<PostJob> PostJobs { get; set; }
        public AppUser AppUser { get; set; }
        public IEnumerable<AppUserPostJob> AppUserPostJobs { get; set; }
        public List<AppUser> StareddUser { get; set; }
        public List<AppUser> AppliedUser { get; set; }
    }
}