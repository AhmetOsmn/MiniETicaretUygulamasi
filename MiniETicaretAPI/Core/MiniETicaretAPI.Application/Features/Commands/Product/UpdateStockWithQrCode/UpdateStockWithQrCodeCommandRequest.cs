using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Product.UpdateStockWithQrCode
{
    public class UpdateStockWithQrCodeCommandRequest : IRequest<UpdateStockWithQrCodeCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}