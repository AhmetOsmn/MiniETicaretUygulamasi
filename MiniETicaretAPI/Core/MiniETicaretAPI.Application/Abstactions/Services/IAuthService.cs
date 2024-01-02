using MiniETicaretAPI.Application.Abstactions.Services.Authentications;

namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
