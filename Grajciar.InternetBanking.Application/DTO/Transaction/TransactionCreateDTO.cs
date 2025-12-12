namespace Grajciar.InternetBanking.Application.DTO.Transaction
{
    public class TransactionCreateDTO
    {
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

        // Relationships
        public int FromAccountId { get; set; }
    }
}
