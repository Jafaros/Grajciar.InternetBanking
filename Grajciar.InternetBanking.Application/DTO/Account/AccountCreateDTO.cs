namespace Grajciar.InternetBanking.Application.DTO.Account
{
    public class AccountCreateDTO
    {
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public int BankId { get; set; }
    }
}
