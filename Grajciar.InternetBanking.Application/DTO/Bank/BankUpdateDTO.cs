using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Application.DTO.Bank
{
    public class BankUpdateDTO
    {
        public string Name { get; set; }
        public string SwiftCode { get; set; }
        public string Address { get; set; }
    }
}
