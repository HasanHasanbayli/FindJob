using FindJob.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindJob.DAL;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Bio> Bios { get; set; }
    public DbSet<PopularJob> PopularJobs { get; set; }
    public DbSet<Statistics> Statistics { get; set; }
    public DbSet<Partners> Partners { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<PostJob> PostJobs { get; set; }
    public DbSet<AppUserPostJob> AppUserPostJobs { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ContactFromUser> ContactFromUsers { get; set; }
    public DbSet<ContactFromAdmin> ContactFromAdmins { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}