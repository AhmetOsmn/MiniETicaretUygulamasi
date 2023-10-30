using MiniETicaretAPI.Application.Dtos.User;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addOnAccesTokenExpireDate);
    }
}
