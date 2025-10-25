

using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ICardAppService
    {
        IList<Card> Select();
        IList<Card> GetByAccount(int accountId);
        Card? Get(int id);
        void Create(Card card);
        bool Delete(int id);
        bool Block(int id);
        bool Unblock(int id);
    }
}
