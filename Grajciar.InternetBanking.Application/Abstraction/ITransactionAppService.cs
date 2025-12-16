using Grajciar.InternetBanking.Application.DTO.Transaction;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ITransactionAppService
    {
        IList<TransactionDTO> GetByAccount(int accountId);
        TransactionDTO? Get(int id);
        List<string> Create(TransactionCreateDTO transaction);
    }
}
