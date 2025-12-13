using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.Implementation;
using Grajciar.InternetBanking.Infrastructure.Database;
using Grajciar.InternetBanking.Infrastructure.Identity;
using Grajciar.InternetBanking.Infrastructure.Security;
using Grajciar.InternetBanking.WebAPI.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access_token"];
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("JWT AUTH FAILED:");
                        Console.WriteLine(context.Exception.ToString());
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"])
                    ),

                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddScoped<IUserAppService, UserAppService>();
            builder.Services.AddScoped<IAccountAppService, AccountAppService>();
            builder.Services.AddScoped<IBankAppService, BankAppService>();
            builder.Services.AddScoped<ICardAppService, CardAppService>();
            builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
            builder.Services.AddScoped<ISecurityService, SecurityAppService>();
            builder.Services.AddScoped<IJWTService, JWTService>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Self", policy =>
                    policy.Requirements.Add(new SelfRequirement()));
            });

            builder.Services.AddScoped<IAuthorizationHandler, SelfHandler>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseCors("Frontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
