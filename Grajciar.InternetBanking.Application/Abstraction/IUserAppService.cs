using Grajciar.InternetBanking.Application.DTO.User;
using Grajciar.InternetBanking.Infrastructure.Identity;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IUserAppService
    {
        IList<UserDTO> Select();
        UserDTO? Get(int id);
        bool Update(int id, UserUpdateDTO dto);
        bool Delete(int id);
    }
}
