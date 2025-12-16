using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Security
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Password { get; set; }
    }

}
