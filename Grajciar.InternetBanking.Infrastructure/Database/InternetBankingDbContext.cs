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

            var userInit = new UserInit();
            modelBuilder.Entity<User>().HasData(userInit.GenerateUsers3());
            var bankInit = new BankInit();
            modelBuilder.Entity<Bank>().HasData(bankInit.GenerateBanks3());
            var accountInit = new AccountInit();
            modelBuilder.Entity<Account>().HasData(accountInit.GenerateAccountsFor3Users());
            var cardInit = new CardInit();
            modelBuilder.Entity<Card>().HasData(cardInit.GenerateCards3());
        }
    }
}
