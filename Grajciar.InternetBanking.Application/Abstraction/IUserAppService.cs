using Grajciar.InternetBanking.Application.DTO.User;
using Grajciar.InternetBanking.Infrastructure.Identity;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IUserAppService
    {
        Task<IList<UserDTO>> Select();
        Task<UserDTO?> Get(int id);
        Task<UserUpdateResponseDTO> Update(int id, UserUpdateDTO dto);
        bool Delete(int id);
    }
}
