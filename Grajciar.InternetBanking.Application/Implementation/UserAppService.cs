using Grajciar.InternetBanking.Application.Abstraction;
using Grajciar.InternetBanking.Application.DTO.User;
using Grajciar.InternetBanking.Domain.Interfaces;
using Grajciar.InternetBanking.Infrastructure.Database;
using Grajciar.InternetBanking.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Grajciar.InternetBanking.Application.Implementation
{
    public class UserAppService : IUserAppService
    {
        InternetBankingDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserAppService(InternetBankingDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IList<UserDTO>> Select()
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Tel = u.Tel,
                    DateOfBirth = u.DateOfBirth,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToListAsync();

            foreach (var user in users)
            {
                var entity = await _userManager.FindByIdAsync(user.Id.ToString());
                user.Roles = (await _userManager.GetRolesAsync(entity)).ToArray();
            }

            return users;
        }

        public async Task<UserDTO?> Get(int id)
        {
            var user = _dbContext.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return null;

            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Tel = user.Tel,
                DateOfBirth = user.DateOfBirth,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            var entity = await _userManager.FindByIdAsync(user.Id.ToString());
            userDTO.Roles = (await _userManager.GetRolesAsync(entity)).ToArray();

            return userDTO;
        }

        public async Task<UserUpdateResponseDTO> Update(int id, UserUpdateDTO dto)
        {
            UserUpdateResponseDTO response = new UserUpdateResponseDTO()
            {
                Success = true,
            };
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) {
                response.Success = false;
                response.Errors.Add("Uživatel neexistuje");
                return response;
            }

            user.UserName = dto.UserName;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Tel = dto.Tel;
            user.UpdatedAt = DateTime.UtcNow;

            _dbContext.SaveChanges();
            response.User = await MapToUserDTO(user);
            return response;
        }

        public bool Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
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
                Roles = roles.ToArray()
            };
        }
    }
}
