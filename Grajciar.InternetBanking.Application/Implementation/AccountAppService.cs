using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class AccountAppService : IAccountAppService
    {
        InternetBankingDbContext _dbContext;
        public AccountAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateForUser(int userId, Account account)
        {
            bool created = false;

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null) {
                account.UserId = userId;
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();
                created = true;
            }

            return created;
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);

            if (account != null) { 
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }

        public Account Get(int id)
        {
            return _dbContext.Accounts.Include(a => a.Transactions).FirstOrDefault(a => a.Id == id);
        }

        public IList<Account> Select()
        {
            return _dbContext.Accounts.ToList();
        }

        public IList<Account> SelectByUser(int userId)
        {
            return _dbContext.Accounts.Where(a => a.UserId == userId).ToList();
        }

        public bool Update(int id, Account account)
        {
            bool updated = false;
            var acc = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);

            if (acc != null) { 
                _dbContext.Accounts.Update(acc);
                _dbContext.SaveChanges();
                updated = true;
            }

            return updated;
        }
    }
}
