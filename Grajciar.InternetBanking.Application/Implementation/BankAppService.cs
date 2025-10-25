using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class BankAppService : IBankAppService
    {
        InternetBankingDbContext _dbContext;
        public BankAppService(InternetBankingDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public void Create(Bank bank)
        {
            _dbContext.Banks.Add(bank);
            _dbContext.SaveChanges();
        }

        public Bank? Get(int id)
        {
            return _dbContext.Banks.FirstOrDefault(b => b.Id == id);
        }

        public IList<Bank> Select()
        {
            return _dbContext.Banks.ToList();
        }

        public bool Update(int id, Bank bank)
        {
            bool updated = false;
            var bank_to_update = _dbContext.Banks.FirstOrDefault(b => b.Id == id);

            if (bank_to_update != null) {
                _dbContext.Banks.Update(bank);
                _dbContext.SaveChanges();
                updated = true;
            }

            return updated;
        }
    }
}
