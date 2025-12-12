using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.User;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        InternetBankingDbContext _dbContext;

        public UserAppService(InternetBankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<UserDTO> Select()
        {
            return _dbContext.Users
                .AsNoTracking()
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Tel = u.Tel,
                    DateOfBirth = u.DateOfBirth,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToList();
        }

        public UserDTO? Get(int id)
        {
            var user = _dbContext.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Tel = user.Tel,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public bool Update(int id, UserUpdateDTO dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Tel = dto.Tel;
            user.UpdatedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
