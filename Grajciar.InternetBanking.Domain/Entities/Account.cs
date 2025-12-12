using Grajciar.InternetBanking.Domain.Enums;
using Grajciar.InternetBanking.Domain.Interfaces;
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
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int UserId { get; set; }
        public IUser User { get; set; }
        public int TypeId { get; set; }
        public BankAccountType Type { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public ICollection<Transaction> OutgoingTransactions { get; set; }
        public ICollection<Transaction> IncomingTransactions { get; set; }
    }
}
