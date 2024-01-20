using MiniETicaretAPI.Application.Dtos.User;
using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IUserService
    {
        int TotalUsersCount { get; }
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenExpireDate, int addOnAccesTokenExpireDate);
        Task UpdatePasswordAsync(string userId, string newPassword ,string resetToken);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrUserName);
        Task<bool> HasRolePermissionToEndpointAsync(string userName, string code);
    }
}
