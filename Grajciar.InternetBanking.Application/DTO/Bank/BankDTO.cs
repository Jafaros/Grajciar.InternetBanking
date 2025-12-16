using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.Bank
{
    public class BankDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

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
