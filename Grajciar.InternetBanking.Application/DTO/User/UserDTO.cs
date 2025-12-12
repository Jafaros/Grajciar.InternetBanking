using Grajciar.InternetBanking.Domain.Enums;

namespace Grajciar.InternetBanking.Application.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Tel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string[] Roles { get; set; }
    }
}
