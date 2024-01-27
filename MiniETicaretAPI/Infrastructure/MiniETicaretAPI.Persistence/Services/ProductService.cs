using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Repositories;
using System.Text.Json;

namespace MiniETicaretAPI.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IQRCodeService _qRCodeService;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qRCodeService)
        {
            _productReadRepository = productReadRepository;
            _qRCodeService = qRCodeService;
        }

        public async Task<byte[]> GenerateQRCodeToProductAsync(string productId)
        {
            Domain.Entities.Product product  = await _productReadRepository.GetByIdAsync(productId) ?? throw new Exception("Product not found");

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate,
            };
            string plainText = JsonSerializer.Serialize(plainObject);
            return _qRCodeService.GenerateQRCode(plainText);
        }
    }
}
