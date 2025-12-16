using Grajciar.InternetBanking.Infrastructure.Identity;
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

            /*var u1 = new User()
            { 
                Id = 3,
                UserName = "petrgrajciar",             
                FirstName = "Petr",
                LastName = "Grajciar",
                Email = "petrgrajciar@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1),
            };

            var u2 = new User()
            {
                Id = 4,
                UserName = "karel",
                FirstName = "Karel",
                LastName = "Chleba",
                Email = "karelchleba@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            var u3 = new User()
            {
                Id = 5,
                UserName = "simon",
                FirstName = "Šimon",
                LastName = "Rohlík",
                Email = "simonrohlik@test.cz",
                Tel = "+420123456789",
                PasswordHash = "hashedpassword123",
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            users.AddRange(u1, u2, u3);*/

            return users;
        }

        public User GetAdmin()
        {
            User admin = new User()
            {
                Id = 1,
                FirstName = "Adminek",
                LastName = "Adminovy",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.cz",
                NormalizedEmail = "ADMIN@ADMIN.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEM9O98Suoh2o2JOK1ZOJScgOfQ21odn/k6EYUpGWnrbevCaBFFXrNL7JZxHNczhh/w==",
                SecurityStamp = "SEJEPXC646ZBNCDYSM3H5FRK5RWP2TN6",
                ConcurrencyStamp = "b09a83ae-cfd3-4ee7-97e6-fbcf0b0fe78c",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            return admin;
        }


        public User GetManager()
        {
            User manager = new User()
            {
                Id = 2,
                FirstName = "Managerek",
                LastName = "Managerovy",
                UserName = "manager",
                Email = "manager@manager.cz",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==",
                SecurityStamp = "MAJXOSATJKOEM4YFF32Y5G2XPR5OFEL6",
                ConcurrencyStamp = "7a8d96fd-5918-441b-b800-cbafa99de97b",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            return manager;
        }

        public User GetCustomer()
        {
            User customer = new User()
            {
                Id = 3,
                FirstName = "Petr",
                LastName = "Grajciar",
                UserName = "petrgrajciar",
                Email = "petr.grajciar@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAECtcRvpkX4e7bdXXGJa8tNlOGJdH/7P9xaiWiBiiKM1yaoCKpWdvtnkJsb/vh4WPEQ==",
                SecurityStamp = "MAJXOSATJKOEM4YFF32Y5G2XPR5OFEL6",
                ConcurrencyStamp = "7a8d96fd-5918-441b-b800-cbafa99de97b",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            return customer;
        }
    }
}
