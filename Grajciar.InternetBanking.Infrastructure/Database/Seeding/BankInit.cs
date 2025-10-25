using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Infrastructure.Database.Seeding
{
    public class BankInit
    {
        public List<Bank> GenerateBanks3()
        {
            List<Bank> banks = new List<Bank>();

            Bank bank1 = new Bank()
            {
                Id = 1,
                Name = "Banka 1",
                BankCode = "0001",
                Address = "Masarykova XYZ, Brno",
                SwiftCode = "BANK1CZ"
            };

            Bank bank2 = new Bank()
            {
                Id = 2,
                Name = "Banka 2",
                BankCode = "0020",
                Address = "Masarykova XYZ, Brno",
                SwiftCode = "BANK2CZ"
            };

            Bank bank3 = new Bank()
            {
                Id = 3,
                Name = "Banka 3",
                BankCode = "0300",
                Address = "Masarykova XYZ, Brno",
                SwiftCode = "BANK3CZ"
            };

            banks.AddRange(bank1, bank2, bank3);

            return banks;
        }
    }
}
