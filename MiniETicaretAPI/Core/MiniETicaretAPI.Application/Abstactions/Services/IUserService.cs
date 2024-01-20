using MiniETicaretAPI.Application.Dtos.User;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addOnAccesTokenExpireDate);
        Task UpdatePasswordAsync(string userId, string newPassword ,string resetToken);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        int TotalUsersCount { get; }
    }
}
