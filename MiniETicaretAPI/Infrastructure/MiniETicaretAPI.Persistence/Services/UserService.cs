using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.User;
using MiniETicaretAPI.Application.Exceptions;
using MiniETicaretAPI.Application.Helpers;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserDto.Username,
                Email = createUserDto.Email,
                NameSurname = createUserDto.NameSurname
            }, createUserDto.Password);

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturuldu";

            else
                foreach (IdentityError? error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}. \n";

            return response;
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            return await _userManager.Users.Skip(page * size).Take(size).Select(x => new ListUser
            {
                Id = x.Id,
                Email = x.Email,
                NameSurname = x.NameSurname,
                UserName = x.UserName,
                TwoFactorEnabled = x.TwoFactorEnabled
            }).ToListAsync();
        }

        public async Task<string[]> GetRolesToUserAsync(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            
            return Array.Empty<string>();
        }

        public async Task UpdatePasswordAsync(string userId, string newPassword, string resetToken)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordUpdateFailedException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addOnAccesTokenExpireDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpireDate = accessTokenExpireDate.AddSeconds(addOnAccesTokenExpireDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new UserNotFoundException();
        }
    }
}
