using Grajciar.InternetBanking.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grajciar.InternetBanking.Domain.Entities
{
    [Table(nameof(Card))]
    public class Card
    {
        public int Id { get; set; }

        // Card details
        [Required]
        [StringLength(16, MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Číslo karty musí obsahovat pouze čísla")]
        public string CardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Bezpečnostní kód musí mít přesně 3 čísla")]
        public string SecurityCode { get; set; }

        [Required]
        public CardType Type { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CardHolderName { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Relationships

        [Required]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;
    }
}
