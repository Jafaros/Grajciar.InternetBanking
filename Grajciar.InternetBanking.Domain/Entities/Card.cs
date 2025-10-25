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
        [MaxLength(16)]
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        [MaxLength(3)]
        public string SecurityCode { get; set; }
        public CardType Type { get; set; }
        public string CardHolderName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
