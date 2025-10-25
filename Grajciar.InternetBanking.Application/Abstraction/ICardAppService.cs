

using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ICardAppService
    {
        IList<Card> GetByAccount(int accountId);
        Card? Get(int id);
        void CreateForAccount(int accountId, Card card);
        bool InitiateCardPayment(int cardId, Transaction transaction);
        bool Delete(int id);
        bool Block(int id);
        bool Unblock(int id);
    }
}
