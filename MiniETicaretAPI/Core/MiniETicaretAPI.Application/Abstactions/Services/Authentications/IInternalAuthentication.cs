namespace MiniETicaretAPI.Application.Abstactions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Dtos.Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime);
    }
}
