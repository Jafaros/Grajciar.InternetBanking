using Grajciar.InternetBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Infrastructure.Database.Seeding
{
    public class AccountInit
    {
        public List<Account> GenerateAccountsFor3Users() { 
            List<Account> accounts = new List<Account>();

            Account account1 = new Account() { 
                Id = 1,
                UserId = 1,
                AccountNumber = "123456789",
                BankId = 1,
                CreatedAt = DateTime.UtcNow,
                Balance = 1000,
                Type = Domain.Enums.BankAccountType.PERSONAL,
            };

            Account account2 = new Account()
            {
                Id = 2,
                UserId = 2,
                AccountNumber = "321654987",
                BankId = 2,
                CreatedAt = DateTime.UtcNow,
                Balance = 500,
                Type = Domain.Enums.BankAccountType.STUDENT,
            };

            Account account3 = new Account()
            {
                Id = 3,
                UserId = 3,
                AccountNumber = "987654321",
                BankId = 3,
                CreatedAt = DateTime.UtcNow,
                Balance = 2000,
                Type = Domain.Enums.BankAccountType.BUSINESS,
            };

            accounts.AddRange(account1, account2, account3);

            return accounts;
        }
    }
}
