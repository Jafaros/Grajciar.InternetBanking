namespace Grajciar.InternetBanking.Application.DTO.User
{
    public class UserCreateDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Tel { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
