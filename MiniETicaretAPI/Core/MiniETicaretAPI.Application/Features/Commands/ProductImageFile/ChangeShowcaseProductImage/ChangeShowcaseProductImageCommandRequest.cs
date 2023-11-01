using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseProductImage
{
    public class ChangeShowcaseProductImageCommandRequest : IRequest
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
