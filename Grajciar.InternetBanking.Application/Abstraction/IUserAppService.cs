using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IUserAppService
    {
        IList<User> Select();
        void Create(User user);
    }
}
