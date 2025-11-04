using Grajciar.InternetBanking.Infrastructure.Identity;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IUserAppService
    {
        IList<User> Select();
        User? Get(int id);
        void Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
