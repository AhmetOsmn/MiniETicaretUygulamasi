using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage
{
    public class DeleteProductImageCommandRequest : IRequest
    {
        public string ProductId { get; set; }
        public string? ImageId { get; set; }
    }
}
