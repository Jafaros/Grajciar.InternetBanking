using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.User
{
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$",
            ErrorMessage = "Uživatelské jméno nesmí obsahovat neplatné znaky.")]
        public string? UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$", ErrorMessage = "Jméno nesmí obsahovat nepovelené znaky")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$", ErrorMessage = "Příjmení nesmí obsahovat nepovolené znaky")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [StringLength(13, MinimumLength = 9)]
        public string? Tel { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
}
