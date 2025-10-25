using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        InternetBankingDbContext _dbContext;
        public UserAppService(InternetBankingDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IList<User> Select()
        {
            return _dbContext.Users.ToList();
        }

        public User Get(int id) {
            return _dbContext.Users.Include(user => user.Accounts).FirstOrDefault(user => user.Id == id);
        }

        public void Create(User user) { 
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public bool Update(int id, User user) {
            bool updated = false;

            User? user_to_update
                = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user_to_update != null)
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                updated = true;
            }

            return updated;
        }

        public bool Delete(int id) {
            bool deleted = false;

            User? user
                = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }
    }
}
