using Grajciar.InternetBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Infrastructure.Database.Seeding
{
    public class BankAccountTypeInit
    {
        public List<BankAccountType> GenerateDefaultTypes()
        {
            List<BankAccountType> types = new List<BankAccountType>();

            BankAccountType type1 = new BankAccountType()
            {
                Id = 1,
                Name = "Osobní",
            };

            BankAccountType type2 = new BankAccountType()
            {
                Id = 2,
                Name = "Spořící",
            };

            BankAccountType type3 = new BankAccountType()
            {
                Id = 3,
                Name = "Podnikatelský",
            };

            BankAccountType type4 = new BankAccountType()
            {
                Id = 4,
                Name = "Studentský",
            };

            types.AddRange(type1, type2, type3, type4);

            return types;
        }
    }
}
