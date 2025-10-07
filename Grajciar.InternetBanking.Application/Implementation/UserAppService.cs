using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Domain.Entities;
using Grajciar.InternetBanking.Infrastructure.Database;

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

        public void Create(User user) { 
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
