using Grajciar.InternetBanking.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Domain.Entities
{
    [Table(nameof(Transaction))]
    public class Transaction
    {
        public int Id { get; set; }

        // Accounts details
        [Required]
        [StringLength(26, MinimumLength = 10)]
        public string FromAccountNumber { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string FromBankCode { get; set; }

        [Required]
        [StringLength(26, MinimumLength = 10)]
        public string ToAccountNumber { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string ToBankCode { get; set; }

        // Symbols

        [StringLength(10)]
        public string? ConstantSymbol { get; set; }

        [StringLength(10)]
        public string? VariableSymbol { get; set; }

        // Transaction details

        [StringLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Částka musí být vyšší než 0")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }

        // Relationships

        public int? FromAccountId { get; set; }

        [ForeignKey(nameof(FromAccountId))]
        public Account? FromAccount { get; set; }

        public int? ToAccountId { get; set; }

        [ForeignKey(nameof(ToAccountId))]
        public Account? ToAccount { get; set; }
    }
}
