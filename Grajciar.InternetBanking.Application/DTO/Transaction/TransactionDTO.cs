namespace Grajciar.InternetBanking.Application.DTO.Transaction
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        // Accounts details
        public string FromAccountNumber { get; set; }
        public string FromBankCode { get; set; }
        public string ToAccountNumber { get; set; }
        public string ToBankCode { get; set; }

        // Symbols
        public string ConstantSymbol { get; set; }
        public string VariableSymbol { get; set; }

        // Transaction details
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }

        // Relationships
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
}
