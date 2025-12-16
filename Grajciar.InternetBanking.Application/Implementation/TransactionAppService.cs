using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Transaction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Domain.Enums;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class TransactionAppService : ITransactionAppService
    {
        InternetBankingDbContext _dbContext;

        public TransactionAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<TransactionDTO> GetByAccount(int accountId)
        {
            return _dbContext.Transactions
                .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
                .Select(t => MapToDTO(t))
                .ToList();
        }

        public TransactionDTO? Get(int id)
        {
            var t = _dbContext.Transactions.FirstOrDefault(t => t.Id == id);
            return t == null ? null : MapToDTO(t);
        }

        public List<string> Create(TransactionCreateDTO dto)
        {
            List<string> errors = new List<string>();

            var fromAccount = _dbContext.Accounts
                .Include(a => a.Bank)
                .FirstOrDefault(a =>
                    a.AccountNumber == dto.FromAccountNumber &&
                    a.Bank.BankCode == dto.FromBankCode);

            var toAccount = _dbContext.Accounts
                .Include(a => a.Bank)
                .FirstOrDefault(a =>
                    a.AccountNumber == dto.ToAccountNumber &&
                    a.Bank.BankCode == dto.ToBankCode);

            if (fromAccount == null || toAccount == null)
            {
                errors.Add("Zadaný účet neexistuje");
                return errors;
            }

            if (fromAccount.Balance < dto.Amount)
            {
                errors.Add("Nemáte dostatečný zůstatek na účtě");
                return errors;
            }


            fromAccount.Balance -= dto.Amount;
            toAccount.Balance += dto.Amount;

            var transaction = MapToEntity(dto);
            transaction.CreatedAt = DateTime.UtcNow;
            transaction.Status = TransactionStatus.SUCCESS;
            transaction.TransactionType = TransactionType.EXPENSE;
            transaction.ToAccountId = toAccount.Id;

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return errors;
        }

        private static TransactionDTO MapToDTO(Transaction t)
        {
            return new TransactionDTO
            {
                Id = t.Id,
                FromAccountNumber = t.FromAccountNumber,
                FromBankCode = t.FromBankCode,
                ToAccountNumber = t.ToAccountNumber,
                ToBankCode = t.ToBankCode,
                ConstantSymbol = t.ConstantSymbol,
                VariableSymbol = t.VariableSymbol,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                TransactionType = t.TransactionType.ToString(),
                Status = t.Status.ToString(),
                ToAccountId = (int) t.ToAccountId,
                FromAccountId = (int) t.FromAccountId,
                Description = t.Description
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
                Description = dto.Description
            };
        }
    }
}
