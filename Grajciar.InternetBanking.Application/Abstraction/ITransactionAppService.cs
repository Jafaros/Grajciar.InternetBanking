using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ITransactionAppService
    {
        IList<Transaction> Select();
        IList<Transaction> GetByAccount(int accountId);
        Transaction? Get(int id);
        void Create(Transaction transaction);
    }
}
