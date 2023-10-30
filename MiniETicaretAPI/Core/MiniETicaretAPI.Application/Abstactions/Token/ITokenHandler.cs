namespace MiniETicaretAPI.Application.Abstactions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int second);
    }
}
