using MiniETicaretAPI.Application.Dtos.User;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
    }
}
