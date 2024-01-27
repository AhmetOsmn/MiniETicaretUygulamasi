namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IQRCodeService
    {
        byte[] GenerateQRCode(string text);
    }
}
