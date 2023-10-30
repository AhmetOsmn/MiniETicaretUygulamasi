namespace MiniETicaretAPI.Application.Abstactions.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<Dtos.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<Dtos.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
