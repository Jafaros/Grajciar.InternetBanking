using Grajciar.InternetBanking.Application.DTO.User;

namespace Grajciar.InternetBanking.Application.DTO.Security
{
    public class AuthResponseDTO
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public UserDTO? User { get; set; }
    }
}
