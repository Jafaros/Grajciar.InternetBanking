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
            builder.Services.AddScoped<IAccountAppService, AccountAppService>();
            builder.Services.AddScoped<IBankAppService, BankAppService>();
            builder.Services.AddScoped<ICardAppService, CardAppService>();
            builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
