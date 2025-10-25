using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IAccountAppService
    {
        Account? Get(int id);
        IList<Account> SelectByUser(int userId);
        bool CreateForUser(int userId, Account account);
        bool Update(int id, Account account);
        bool Delete(int id);
    }
}
