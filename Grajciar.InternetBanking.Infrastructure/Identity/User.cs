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
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Phone]
        public string? Tel { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        public ICollection<Account> Accounts { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
