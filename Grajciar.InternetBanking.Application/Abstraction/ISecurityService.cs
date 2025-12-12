using Grajciar.InternetBanking.Application.DTO.Security;
using Grajciar.InternetBanking.Infrastructure.Identity.Enums;
namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface ISecurityService
    {
        Task<AuthResponseDTO> Login(LoginDTO dto);
        Task<RegisterResponseDTO> Register(RegisterDTO dto, params Roles[] roles);
    }
}
