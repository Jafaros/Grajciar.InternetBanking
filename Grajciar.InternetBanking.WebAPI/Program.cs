using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.Implementation;
using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MySQL");
            ServerVersion serverVersion = new MySqlServerVersion("8.0.43");
            builder.Services.AddDbContext<InternetBankingDbContext>(options => options.UseMySql(connectionString, serverVersion));
            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserAppService, UserAppService>();

            var app = builder.Build();

            app.MapControllers();

            app.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                  name: "home_area_get",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                  name: "user_area_get",
                  pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                  name: "user_area_create",
                  pattern: "{area:exists}/{controller=User}/{action=Create}"
            );

            app.Run();
        }
    }
}
