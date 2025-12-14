namespace Grajciar.InternetBanking.Application.DTO.Account
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public int BankId { get; set; }
        public string BankCode { get; set; }
        public string Type { get; set; }
    }
}
