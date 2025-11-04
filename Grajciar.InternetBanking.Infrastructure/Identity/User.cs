using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Domain.Enums;
using Grajciar.InternetBanking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Infrastructure.Identity
{
    [Table(nameof(User))]
    public class User : IdentityUser<int>, IUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Tel { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public UserType UserType { get; set; } = UserType.USER;

        // Relationships
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
