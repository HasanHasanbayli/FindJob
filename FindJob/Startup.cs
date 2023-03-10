using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recruitment.DAL;
using Recruitment.Hubs;
using Recruitment.Models;

namespace Recruitment;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(_config["ConnectionString:Default"]); });
        services.AddIdentity<AppUser, IdentityRole>(identityOptions =>
        {
            identityOptions.Password.RequiredLength = 6;
            identityOptions.Password.RequireNonAlphanumeric = false;
            identityOptions.Password.RequireLowercase = false;
            identityOptions.Password.RequireUppercase = false;
            identityOptions.Password.RequireDigit = true;
            identityOptions.User.RequireUniqueEmail = true;
            identityOptions.Lockout.AllowedForNewUsers = true;
            identityOptions.Lockout.MaxFailedAccessAttempts = 3;
            identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "areas",
                "{area:exists}/{controller=Bio}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHub<ChatHub>("/chathub");
        });
    }
}