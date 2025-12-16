using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Account;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;
using Grajciar.InternetBanking.Infrastructure.Identity;
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

        public IList<AccountDTO> Select()
        {
            return _dbContext.Accounts
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    Balance = a.Balance,
                    AccountNumber = a.AccountNumber,
                    CreatedAt = a.CreatedAt,
                    UserId = a.UserId,
                    TypeId = a.TypeId,
                    BankId = a.BankId,
                    BankCode = a.Bank.BankCode,
                    Type = a.Type.Name
                })
                .ToList();
        }

        public IList<AccountDTO> SelectByUser(int userId)
        {
            return _dbContext.Accounts
                .Where(a => a.UserId == userId)
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    Balance = a.Balance,
                    AccountNumber = a.AccountNumber,
                    CreatedAt = a.CreatedAt,
                    UserId = a.UserId,
                    TypeId = a.TypeId,
                    BankId = a.BankId,
                    BankCode = a.Bank.BankCode,
                    Type = a.Type.Name
                })
                .ToList();
        }

        public AccountDTO? Get(int id)
        {
            return _dbContext.Accounts
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AccountDTO
                {
                    Id = a.Id,
                    Balance = a.Balance,
                    AccountNumber = a.AccountNumber,
                    CreatedAt = a.CreatedAt,
                    UserId = a.UserId,
                    TypeId = a.TypeId,
                    BankId = a.BankId,
                    BankCode = a.Bank.BankCode,
                    Type = a.Type.Name
                })
                .FirstOrDefault();
        }

        public bool CreateForUser(int userId, AccountCreateDTO dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;

            var account = new Account
            {
                Balance = dto.Balance,
                AccountNumber = dto.AccountNumber,
                UserId = userId,
                TypeId = dto.TypeId,
                BankId = dto.BankId,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(int id, AccountUpdateDTO dto)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
                return false;

            account.AccountNumber = dto.AccountNumber;
            account.TypeId = dto.TypeId;
            account.BankId = dto.BankId;
            account.Balance = dto.Balance;

            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
                return false;

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
