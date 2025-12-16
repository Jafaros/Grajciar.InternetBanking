using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Transaction
{
    public class TransactionCreateDTO
    {
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
        public string ConstantSymbol { get; set; }

        [StringLength(10)]
        public string VariableSymbol { get; set; }

        // Transaction details
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hodnota musí být vyšší než 0")]
        public decimal Amount { get; set; }

        // Relationships
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ID odchozího účtu musí být validní")]
        public int FromAccountId { get; set; }
    }
}
