using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Infrastructure.Identity
{
    [Table(nameof(User))]
    public class User : IdentityUser<int>
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }

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
