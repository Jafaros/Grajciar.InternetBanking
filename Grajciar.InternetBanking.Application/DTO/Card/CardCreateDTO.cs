using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Application.DTO.Card
{
    public class CardCreateDTO
    {
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsBlocked { get; set; }
        public int AccountId { get; set; }
    }
}
