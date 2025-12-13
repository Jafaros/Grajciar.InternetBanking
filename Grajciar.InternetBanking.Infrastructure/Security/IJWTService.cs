using Grajciar.InternetBanking.Infrastructure.Identity;

namespace Grajciar.InternetBanking.Infrastructure.Security
{
    public interface IJWTService
    {
        public Task<string> CreateToken(User user);
    }
}
