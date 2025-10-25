using Grajciar.InternetBanking.Domain.Entities;

namespace Grajciar.InternetBanking.Application.Abstraction
{
    public interface IBankAppService
    {
        IList<Bank> Select();
        Bank? Get(int id);
        void Create(Bank bank);
        bool Update(int id, Bank bank);
        bool Delete(int id);
    }
}
