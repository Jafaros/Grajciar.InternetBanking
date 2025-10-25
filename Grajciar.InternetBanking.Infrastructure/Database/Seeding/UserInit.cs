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
                LastName = "Grajciar",
                Email = "petrgrajciar@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1),
            };

            var u2 = new User()
            {
                Id = 2,
                Username = "karel",
                FirstName = "Karel",
                LastName = "Chleba",
                Email = "karelchleba@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            var u3 = new User()
            {
                Id = 3,
                Username = "simon",
                FirstName = "Šimon",
                LastName = "Rohlík",
                Email = "simonrohlik@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            users.AddRange(u1, u2, u3);

            return users;
        }
    }
}
