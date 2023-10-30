using Microsoft.AspNetCore.Identity;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.User;
using MiniETicaretAPI.Application.Exceptions;
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

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addOnAccesTokenExpireDate)
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
