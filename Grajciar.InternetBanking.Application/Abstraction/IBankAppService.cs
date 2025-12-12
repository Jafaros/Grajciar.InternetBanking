using Grajciar.InternetBanking.Application.DTO.Bank;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IBankAppService
    {
        IList<BankDTO> Select();
        BankDTO? Get(int id);
        void Create(BankCreateDTO bank);
        bool Update(int id, BankUpdateDTO bank);
    }
}
