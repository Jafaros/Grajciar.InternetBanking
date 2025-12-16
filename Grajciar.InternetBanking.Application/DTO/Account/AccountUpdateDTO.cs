using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Account
{
    public class AccountUpdateDTO
    {
        [Required]
        [StringLength(26, MinimumLength = 10)]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Zůstatek nemůže být záporný.")]
        public decimal Balance { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BankId { get; set; }
    }
}
