using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class CardAppService : ICardAppService
    {

        InternetBankingDbContext _dbContext;
        public CardAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Block(int id)
        {
            bool blocked = false;
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);

            if (card != null) {
                card.IsBlocked = true;
                _dbContext.SaveChanges();
                blocked = true;
            }

            return blocked;
        }

        public void CreateForAccount(int accountId, Card card)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);

            if (account != null) {
                card.AccountId = accountId;
                _dbContext.Cards.Add(card);
                _dbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);

            if (card != null) { 
                _dbContext.Cards.Remove(card);
                _dbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }

        public Card? Get(int id)
        {
            return _dbContext.Cards.FirstOrDefault(_c => _c.Id == id);
        }

        public IList<Card> GetByAccount(int accountId)
        {
            return _dbContext.Cards.Where(c => c.AccountId == accountId).ToList();
        }

        public bool InitiateCardPayment(int cardId, Transaction transaction)
        {
            bool paid = false;
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == cardId);

            if (card == null) {
                return paid;
            }

            if (card.IsBlocked) {
                return paid;
            }

            var fromAccount = card.Account;
            if (fromAccount == null) {
                return paid;
            }

            if (card.Type == Domain.Enums.CardType.DEBIT) {
                if (fromAccount.Balance < transaction.Amount) {
                    return paid;
                }
            }

            var toAccount = _dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == transaction.ToAccountNumber && a.Bank.BankCode == transaction.ToBankCode);

            if (toAccount == null) {
                return paid;
            }

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;
            transaction.AccountId = fromAccount.Id;
            transaction.TransactionType = Domain.Enums.TransactionType.EXPENSE;
            
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return paid;
        }

        public bool Unblock(int id)
        {
            bool unblocked = false;
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);

            if (card != null)
            {
                card.IsBlocked = false;
                _dbContext.SaveChanges();
                unblocked = true;
            }

            return unblocked;
        }
    }
}
