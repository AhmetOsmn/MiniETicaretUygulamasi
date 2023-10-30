using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class UpdateProductCommandRequest : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
