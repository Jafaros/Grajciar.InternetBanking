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
        public string FromAccountNumber { get; set; }
        public string FromBankCode { get; set; }
        public string ToAccountNumber { get; set; }
        public string ToBankCode { get; set; }

        // Symbols
        [MaxLength(10)]
        public string ConstantSymbol { get; set; }
        [MaxLength(10)]
        public string VariableSymbol { get; set; }

        // Transaction details
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionStatus Status { get; set; }

        // Relationships
        public int? FromAccountId { get; set; }
        public Account? FromAccount { get; set; }

        public int? ToAccountId { get; set; }
        public Account? ToAccount { get; set; }
    }
}
