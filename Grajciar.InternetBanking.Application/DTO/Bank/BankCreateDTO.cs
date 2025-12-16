using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Application.DTO.Bank
{
    public class BankCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 8)]
        [RegularExpression(
            @"^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$",
            ErrorMessage = "Neplatný kód SWIFT"
        )]
        public string SwiftCode { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string BankCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }
    }
}
