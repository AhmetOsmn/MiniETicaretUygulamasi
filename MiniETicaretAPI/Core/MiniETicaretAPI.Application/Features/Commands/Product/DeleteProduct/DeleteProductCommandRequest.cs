using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest
    {
        public string Id { get; set; }
    }
}
