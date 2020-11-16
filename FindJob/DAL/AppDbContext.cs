using FindJob.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindJob.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<PopularJobCategories> PopularJobCategories { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<PostJob> PostJobs { get; set; }
    }
}
