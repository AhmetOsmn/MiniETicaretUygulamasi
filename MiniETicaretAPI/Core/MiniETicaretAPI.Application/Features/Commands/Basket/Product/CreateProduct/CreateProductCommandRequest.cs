using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Basket.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
