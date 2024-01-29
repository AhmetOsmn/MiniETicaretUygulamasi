using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.Product.UpdateStockWithQrCode
{
    public class UpdateStockWithQrCodeCommandHandler : IRequestHandler<UpdateStockWithQrCodeCommandRequest, UpdateStockWithQrCodeCommandResponse>
    {
        private readonly IProductService _productService;

        public UpdateStockWithQrCodeCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateStockWithQrCodeCommandResponse> Handle(UpdateStockWithQrCodeCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.UpdateStockWithQrCodeAsync(request.ProductId, request.Stock);
            return new();
        }
    }
}
