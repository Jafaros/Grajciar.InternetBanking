using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Infrastructure.Database
{
    public class InternetBankingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public InternetBankingDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userInit = new UserInit();
            modelBuilder.Entity<User>().HasData(userInit.GenerateUsers3());
        }
    }
}
