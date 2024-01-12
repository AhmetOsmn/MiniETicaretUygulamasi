using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;

namespace MiniETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;

        public CompleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.CompleteOrderAsync(request.Id);

            return new();
        }
    }
}
