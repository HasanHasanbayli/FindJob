using Recruitment.DAL;
using Recruitment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.Helpers
{
    public static class AddFavorites
    {
        //public async static Task<bool> AddFavorite(int Id, AppDbContext _db, AppUser user)
        //{
        //    var auction = _db.PostJobs.Include(x => x.AppUserPostJobs).FirstOrDefault(x => x.Id == Id);

        //    var isFavorite = auction.AppUserPostJobs.Where(x => x.IsFavorite == true).FirstOrDefault(x => x.AppUserId == user.Id);


        //    return true;
        //}
    }
}
