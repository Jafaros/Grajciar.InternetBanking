using Grajciar.InternetBanking.Application.DTO;
using Grajciar.InternetBanking.Application.DTO.Card;
using Grajciar.InternetBanking.Application.DTO.Transaction;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ICardAppService
    {
        IList<CardDTO> GetByAccount(int accountId);
        CardDTO? Get(int id);
        Task CreateForAccount(int accountId, CardCreateDTO cardDto);
        bool InitiateCardPayment(int cardId, TransactionCreateDTO transaction);
        bool Delete(int id);
        bool Block(int id);
        bool Unblock(int id);
    }
}
