using MiniETicaretAPI.Domain.Entities.Identity;

namespace MiniETicaretAPI.Application.Abstactions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
