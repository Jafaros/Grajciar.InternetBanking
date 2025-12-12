using Grajciar.InternetBanking.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class JWTService
    {
        IConfiguration _config;
        IConfigurationSection _jwtSettings;

        public JWTService(IConfiguration config) {
            _config = config;
            _jwtSettings = _config.GetSection("JWTSettings");
        }

        public string CreateToken(User user) {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["Key"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(User user) {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["ExpiresInMinutes"])),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }
    }
}
