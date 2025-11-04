using Microsoft.AspNetCore.Identity;

namespace Grajciar.InternetBanking.Infrastructure.Identity
{
    public class Role: IdentityRole<int>
    {
        public Role(string role): base(role) { }

        public Role() : base() { }
    }
}
