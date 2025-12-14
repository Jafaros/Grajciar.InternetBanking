namespace Grajciar.InternetBanking.Application.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsBlocked { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
        public int AccountId { get; set; }
        public int TypeId { get; set; }
    }
}
