using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database.Seeding;
using Grajciar.InternetBanking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = Grajciar.InternetBanking.Infrastructure.Identity.User;

namespace Grajciar.InternetBanking.Infrastructure.Database
{
    public class InternetBankingDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Bank> Banks { get; set; }

        public InternetBankingDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RolesInit rolesInit = new RolesInit();
            modelBuilder.Entity<Role>().HasData(rolesInit.GetRolesAMC());

            UserInit userInit = new UserInit();
            User admin = userInit.GetAdmin();
            User manager = userInit.GetManager();

            modelBuilder.Entity<User>().HasData(admin, manager, userInit.GenerateUsers3());

            UserRolesInit userRolesInit = new UserRolesInit();
            List<IdentityUserRole<int>> adminUserRoles = userRolesInit.GetRolesForAdmin();
            List<IdentityUserRole<int>> managerUserRoles = userRolesInit.GetRolesForManager();
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRoles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(managerUserRoles);


            var bankInit = new BankInit();
            modelBuilder.Entity<Bank>().HasData(bankInit.GenerateBanks3());
            var accountInit = new AccountInit();
            modelBuilder.Entity<Account>().HasData(accountInit.GenerateAccountsFor3Users());
            var cardInit = new CardInit();
            modelBuilder.Entity<Card>().HasData(cardInit.GenerateCards3());       
        }
    }
}
