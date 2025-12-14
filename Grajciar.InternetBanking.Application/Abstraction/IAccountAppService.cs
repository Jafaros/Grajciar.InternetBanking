using Grajciar.InternetBanking.Application.DTO.Account;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IAccountAppService
    {
        IList<AccountDTO> Select();
        AccountDTO? Get(int id);
        IList<AccountDTO> SelectByUser(int userId);
        bool CreateForUser(int userId, AccountCreateDTO account);
        bool Update(int id, AccountUpdateDTO account);
        bool Delete(int id);
    }
}
