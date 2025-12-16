using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO
{
    public class CardDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 13)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string SecurityCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CardHolderName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AccountId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TypeId { get; set; }
    }
}
