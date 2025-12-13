using Grajciar.InternetBanking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Infrastructure.Security
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<User> _userManager;

        public JWTService(IConfiguration config, UserManager<User> userManager) {
            _config = config;
            _jwtSettings = _config.GetSection("JWTSettings");
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user) {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["Key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(User user) {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings["ExpiresInMinutes"])),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }
    }
}
