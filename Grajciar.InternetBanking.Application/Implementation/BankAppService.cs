using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Bank;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class BankAppService : IBankAppService
    {
        InternetBankingDbContext _dbContext;

        public BankAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(BankCreateDTO bankDto)
        {
            var entity = MapToEntity(bankDto);

            _dbContext.Banks.Add(entity);
            _dbContext.SaveChanges();
        }

        public BankDTO? Get(int id)
        {
            var bank = _dbContext.Banks.FirstOrDefault(b => b.Id == id);
            return bank != null ? MapToDTO(bank) : null;
        }

        public IList<BankDTO> Select()
        {
            return _dbContext.Banks
                .Select(b => MapToDTO(b))
                .ToList();
        }

        public bool Update(int id, BankUpdateDTO bankDto)
        {
            var existing = _dbContext.Banks.FirstOrDefault(b => b.Id == id);

            if (existing == null)
                return false;

            existing.Name = bankDto.Name;
            existing.SwiftCode = bankDto.SwiftCode;
            existing.Address = bankDto.Address;

            _dbContext.SaveChanges();
            return true;
        }

        private static BankDTO MapToDTO(Bank bank)
        {
            return new BankDTO
            {
                Id = bank.Id,
                Name = bank.Name,
                SwiftCode = bank.SwiftCode,
                Address = bank.Address,
                BankCode = bank.BankCode
            };
        }

        private static Bank MapToEntity(BankCreateDTO dto)
        {
            return new Bank
            {
                Name = dto.Name,
                SwiftCode = dto.SwiftCode,
                Address = dto.Address,
                BankCode = dto.BankCode
            };
        }
    }
}
