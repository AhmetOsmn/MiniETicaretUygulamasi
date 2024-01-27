namespace MiniETicaretAPI.Application.Abstactions.Services
{
    public interface IProductService
    {
        Task<byte[]> GenerateQRCodeToProductAsync(string productId);
    }
}
