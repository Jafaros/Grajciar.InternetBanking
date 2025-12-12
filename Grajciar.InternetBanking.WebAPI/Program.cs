using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.Implementation;
using Grajciar.InternetBanking.Infrastructure.Database;
using Grajciar.InternetBanking.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<InternetBankingDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.User.RequireUniqueEmail = true;
            });

            var jwtSettings = builder.Configuration.GetSection("JWTSettings");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"])
                    )
                };
            });

            builder.Services.AddScoped<IUserAppService, UserAppService>();
            builder.Services.AddScoped<IAccountAppService, AccountAppService>();
            builder.Services.AddScoped<IBankAppService, BankAppService>();
            builder.Services.AddScoped<ICardAppService, CardAppService>();
            builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
            builder.Services.AddScoped<ISecurityService, SecurityAppService>();
            builder.Services.AddSingleton<JWTService>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
