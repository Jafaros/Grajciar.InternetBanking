using Grajciar.InternetBanking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            var connectionString = builder.Configuration.GetConnectionString("MySQL");
            ServerVersion serverVersion = new MySqlServerVersion("8.0.43");
            builder.Services.AddDbContext<InternetBankingDbContext>(options => options.UseMySql(connectionString, serverVersion));

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
