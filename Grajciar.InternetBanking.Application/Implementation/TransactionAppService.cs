using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class TransactionAppService : ITransactionAppService
    {
        InternetBankingDbContext _dbContext;
        public TransactionAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Transaction transaction)
        {
            var fromAccount = _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == transaction.FromAccountNumber && a.Bank.BankCode == transaction.FromBankCode);
            var toAccount = _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == transaction.ToAccountNumber && a.Bank.BankCode == transaction.ToBankCode);

            if (fromAccount == null || toAccount == null)
                throw new Exception("One or both accounts do not exist.");

            if (fromAccount.Balance < transaction.Amount)
                throw new Exception("Insufficient funds.");

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;

            transaction.CreatedAt = DateTime.UtcNow;
            transaction.Status = Domain.Enums.TransactionStatus.SUCCESS;

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public Transaction? Get(int id)
        {
            return _dbContext.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public IList<Transaction> GetByAccount(int accountId)
        {
            return _dbContext.Transactions.Where(tr => tr.AccountId == accountId).ToList();
        }
    }
}
