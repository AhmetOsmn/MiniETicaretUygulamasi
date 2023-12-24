using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.Product.Order.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
