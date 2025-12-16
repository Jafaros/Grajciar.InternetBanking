using System.ComponentModel.DataAnnotations;

namespace Grajciar.InternetBanking.Application.DTO.BankAccountType
{
    public class BankAccountTypeDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
    }

}
