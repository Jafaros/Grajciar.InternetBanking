using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO;
using Grajciar.InternetBanking.Application.DTO.Card;
using Grajciar.InternetBanking.Application.DTO.Transaction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Domain.Enums;
using Grajciar.InternetBanking.Infrastructure.Database;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class CardAppService : ICardAppService
    {
        InternetBankingDbContext _dbContext;

        public CardAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateForAccount(int accountId, CardCreateDTO cardDto)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                return 0;

            var card = MapToEntity(cardDto);
            card.AccountId = accountId;

            _dbContext.Cards.Add(card);
            _dbContext.SaveChanges();

            return card.Id;
        }

        public CardDTO? Get(int id)
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);
            return card != null ? MapToDTO(card) : null;
        }

        public IList<CardDTO> GetByAccount(int accountId)
        {
            return _dbContext.Cards
                .Where(c => c.AccountId == accountId)
                .Select(c => MapToDTO(c))
                .ToList();
        }

        public bool Block(int id)
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);
            if (card == null) return false;

            card.IsBlocked = true;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Unblock(int id)
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);
            if (card == null) return false;

            card.IsBlocked = false;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == id);
            if (card == null) return false;

            _dbContext.Cards.Remove(card);
            _dbContext.SaveChanges();
            return true;
        }

        public bool InitiateCardPayment(int cardId, TransactionCreateDTO dto)
        {
            var card = _dbContext.Cards.FirstOrDefault(c => c.Id == cardId);
            if (card == null) return false;

            if (card.IsBlocked) return false;

            var fromAccount = card.Account;
            if (fromAccount == null) return false;

            var transaction = MapToEntity(dto);

            if (card.Type == Domain.Enums.CardType.DEBIT &&
                fromAccount.Balance < transaction.Amount)
            {
                return false;
            }

            var toAccount = _dbContext.Accounts.FirstOrDefault(a =>
                a.AccountNumber == transaction.ToAccountNumber &&
                a.Bank.BankCode == transaction.ToBankCode);

            if (toAccount == null) return false;

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;

            transaction.FromAccountId = fromAccount.Id;

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return true;
        }

        private CardDTO MapToDTO(Card card)
        {
            return new CardDTO
            {
                Id = card.Id,
                CardNumber = card.CardNumber,
                ExpirationDate = card.ExpirationDate,
                IsBlocked = card.IsBlocked,
                AccountId = card.AccountId
            };
        }

        private Card MapToEntity(CardCreateDTO dto)
        {
            return new Card
            {
                CardNumber = dto.CardNumber,
                ExpirationDate = dto.ExpirationDate,
                IsBlocked = dto.IsBlocked,
                AccountId = dto.AccountId
            };
        }

        private Transaction MapToEntity(TransactionCreateDTO dto)
        {
            return new Transaction
            {
                FromAccountNumber = dto.FromAccountNumber,
                FromBankCode = dto.FromBankCode,
                ToAccountNumber = dto.ToAccountNumber,
                ToBankCode = dto.ToBankCode,
                ConstantSymbol = dto.ConstantSymbol,
                VariableSymbol = dto.VariableSymbol,
                Amount = dto.Amount,
                FromAccountId = dto.FromAccountId,
                CreatedAt = DateTime.UtcNow,
                Status = TransactionStatus.SUCCESS,
                TransactionType = TransactionType.EXPENSE
            };
        }
    }
}
