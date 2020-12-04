using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.Models
{
    public class AppUserPostJob
    {
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsContacted { get; set; }
        public string AppendUserId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int PostJobId { get; set; }
        public PostJob PostJob { get; set; }
    }
}
