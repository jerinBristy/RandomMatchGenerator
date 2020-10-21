using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge2.Models
{
    public class UserDBContext:DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { 
        
        }
        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<PossessionTitleDataModel> PossessionTitle { get; set; }
        public DbSet<TitleMasterModel> TitleMaster { get; set; }
        public DbSet<PlayerHistoryModel> PlayerHistory { get; set; }

    }
}
