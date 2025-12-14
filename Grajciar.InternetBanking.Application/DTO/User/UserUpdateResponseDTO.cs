namespace Grajciar.InternetBanking.Application.DTO.User
{
    public class UserUpdateResponseDTO
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
        public UserDTO User {get; set;}
    }
}
