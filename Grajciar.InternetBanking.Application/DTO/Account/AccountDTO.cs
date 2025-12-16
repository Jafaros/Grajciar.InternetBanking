using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Account
{
    public class AccountDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Zůstatek nemůže být záporný.")]
        public decimal Balance { get; set; }

        [Required]
        [StringLength(26, MinimumLength = 10)]
        public string AccountNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BankId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
