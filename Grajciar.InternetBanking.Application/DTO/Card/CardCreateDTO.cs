using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Application.DTO.Card
{
    public class CardCreateDTO
    {
        [Required]
        [StringLength(16, MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Číslo karty musí obsahovat pouze číslice")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Bezpečnostní kód musí obsahovat přesně 3 čísla")]
        public string SecurityCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public bool IsBlocked { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AccountId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TypeId { get; set; }
    }
}
