using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Infrastructure.Database.Seeding
{
    public class CardInit
    {
        public List<Card> GenerateCards3() { 
            List<Card> cards = new List<Card>();

            Card card1 = new Card()
            {
                Id = 1,
                AccountId = 1,
                CardNumber = "0123456789012345",
                CardHolderName = "Petr Grajciar",
                ExpirationDate = new DateTime(2026, 11, 10),
                SecurityCode = "123",
                Type = Domain.Enums.CardType.DEBIT,
                CreatedAt = DateTime.UtcNow,
            };

            Card card2 = new Card()
            {
                Id = 2,
                AccountId = 2,
                CardNumber = "1234951274563654",
                CardHolderName = "Karel Chleba",
                ExpirationDate = new DateTime(2027, 8, 2),
                SecurityCode = "213",
                Type = Domain.Enums.CardType.CREDIT,
                CreatedAt = DateTime.UtcNow,
            };

            Card card3 = new Card()
            {
                Id = 3,
                AccountId = 3,
                CardNumber = "1478852365891452",
                CardHolderName = "Šimon Rohlík",
                ExpirationDate = new DateTime(2025, 10, 20),
                SecurityCode = "231",
                Type = Domain.Enums.CardType.DEBIT,
                CreatedAt = DateTime.UtcNow,
            };

            cards.AddRange(card1, card2, card3);

            return cards;
        }
    }
}
