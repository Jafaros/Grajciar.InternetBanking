using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Infrastructure.Identity
{
    [Table(nameof(User))]
    public class User : IdentityUser<int>, IUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$", ErrorMessage = "Jméno nesmí obsahovat nepovelené znaky")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-ZÀ-ž\s'-]+$", ErrorMessage = "Příjmení nesmí obsahovat nepovolené znaky")]
        public string LastName { get; set; }

        [Phone]
        [StringLength(13, MinimumLength = 9)]
        public string? Tel { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(ValidateDateOfBirth))]

        public string? ProfileImagePath { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        public ICollection<Account> Accounts { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public static ValidationResult? ValidateDateOfBirth(DateTime date, ValidationContext context)
        {
            if (date > DateTime.UtcNow)
                return new ValidationResult("Datum narození nemůže být v budoucnosti.");

            if (date < DateTime.UtcNow.AddYears(-130))
                return new ValidationResult("Datum narození je neplatné.");

            return ValidationResult.Success;
        }
    }
}
