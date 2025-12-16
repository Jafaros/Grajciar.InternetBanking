using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.Security;
using Grajciar.InternetBanking.Application.DTO.User;
using Grajciar.InternetBanking.Infrastructure.Identity;
using Grajciar.InternetBanking.Infrastructure.Identity.Enums;
using Grajciar.InternetBanking.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class SecurityAppService : ISecurityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IJWTService _jwtService;

        public SecurityAppService(UserManager<User> userManager, RoleManager<Role> roleManager, IJWTService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO dto)
        {
            AuthResponseDTO response = new AuthResponseDTO() { Success = true };

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                response.Success = false;
                response.ErrorMessage = "Neplatné přihlašovací údaje";
                return response;
            }

            var token = await _jwtService.CreateToken(user);
            response.Token = token;
            response.User = await MapToUserDTO(user);
            return response;
        }

        public async Task<RegisterResponseDTO> Register(RegisterDTO dto, params Roles[] roles)
        {
            RegisterResponseDTO response = new RegisterResponseDTO() { Success = true };

            var user = new User
            {
                Email = dto.Email,
                Tel = dto.Tel,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                CreatedAt = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                response.Success = false;
                response.Errors = errors;
            }

            foreach (var role in roles)
            {
                var roleName = role.ToString();

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new Role(roleName));
                }

                var roleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!roleResult.Succeeded)
                {
                    response.Success = false;
                    response.Errors = roleResult.Errors.Select(e => e.Description);
                }
            }

            return response;
        }

        private async Task<UserDTO> MapToUserDTO(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Tel = user.Tel,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = roles.ToArray(),
                ProfileImagePath = user.ProfileImagePath
            };
        }
    }
}
