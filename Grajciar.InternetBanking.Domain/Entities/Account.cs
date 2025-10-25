using Grajciar.InternetBanking.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Domain.Entities
{
    [Table(nameof(Account))]
    public class Account
    {
        public int Id { get; set; }

        // Account balance
        public decimal Balance { get; set; }

        // Account details
        public string AccountNumber { get; set; }
        public BankAccountType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public int UserId { get; set; }
        public User User { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public IList<Card> Cards { get; set; } = new List<Card>();
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
