using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Security
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Phone]
        [StringLength(13, MinimumLength = 9)]
        public string? Tel { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$",
            ErrorMessage = "Uživatelské jméno nesmí obsahovat neplatné znaky.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$",
            ErrorMessage = "Jméno nesmí obsahovat neplatné znaky.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$",
            ErrorMessage = "Příjmení nesmí obsahovat neplatné znaky.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
