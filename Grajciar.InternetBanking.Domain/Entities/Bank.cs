using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Domain.Entities
{
    public class Bank
    {
        public int Id { get; set; }

        // Bank details
        public string Name { get; set; }
        public string BankCode { get; set; }
        public string Address { get; set; }
        public string SwiftCode { get; set; }
        
        // Relationships
        public IList<Account> Accounts { get; set; } = new List<Account>();
    }
}
