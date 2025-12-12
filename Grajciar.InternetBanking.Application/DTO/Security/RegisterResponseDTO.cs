namespace Grajciar.InternetBanking.Application.DTO.Security
{
    public class RegisterResponseDTO
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
