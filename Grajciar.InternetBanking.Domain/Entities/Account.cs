using Grajciar.InternetBanking.Domain.Enums;
using Grajciar.InternetBanking.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Domain.Entities
{
    [Table(nameof(Account))]
    public class Account
    {
        public int Id { get; set; }

        // Account balance
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Stav účtu nemůže být záporný")]
        public decimal Balance { get; set; }

        // Account details
        [Required]
        [StringLength(26, MinimumLength = 10)]
        public string AccountNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IUser User { get; set; }

        [Required]
        public int TypeId { get; set; }

        [ForeignKey(nameof(TypeId))]
        public BankAccountType Type { get; set; } = null!;

        [Required]
        public int BankId { get; set; }

        [ForeignKey(nameof(BankId))]
        public Bank Bank { get; set; } = null!;

        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public ICollection<Transaction> OutgoingTransactions { get; set; }
        public ICollection<Transaction> IncomingTransactions { get; set; }
    }
}
