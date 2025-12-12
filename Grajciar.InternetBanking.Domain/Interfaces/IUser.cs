using Grajciar.InternetBanking.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Domain.Interfaces
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Tel { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        public ICollection<Account> Accounts { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
