using Grajciar.InternetBanking.Domain.Enums;

namespace Grajciar.InternetBanking.Application.DTO.User
{
    public class UserUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
