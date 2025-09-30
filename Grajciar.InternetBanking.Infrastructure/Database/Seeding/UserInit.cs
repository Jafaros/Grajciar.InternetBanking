using Grajciar.InternetBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Infrastructure.Database.Seeding
{
    public class UserInit
    {
        public List<User> GenerateUsers3() 
        { 
            List<User> users = new List<User>();

            var u1 = new User()
            { 
                Id = 1,
                Username = "petrgrajciar",
                FirstName = "Petr",
                LastName = "Grajciar"
            };

            var u2 = new User()
            {
                Id = 2,
                Username = "karel",
                FirstName = "Karel",
                LastName = "Chleba"
            };

            var u3 = new User()
            {
                Id = 3,
                Username = "simon",
                FirstName = "Šimon",
                LastName = "Rohlík"
            };

            users.AddRange(u1, u2, u3);

            return users;
        }
    }
}
