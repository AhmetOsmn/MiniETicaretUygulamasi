using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Basket.Product.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest
    {
        public string Id { get; set; }
    }
}
