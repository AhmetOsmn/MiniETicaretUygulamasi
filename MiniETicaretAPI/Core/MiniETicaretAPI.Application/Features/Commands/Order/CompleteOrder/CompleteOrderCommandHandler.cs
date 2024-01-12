using MediatR;
using MiniETicaretAPI.Application.Abstactions.Services;
using MiniETicaretAPI.Application.Dtos.Order;

namespace MiniETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            CompletedOrder order = await _orderService.CompleteOrderAsync(request.Id);

            if(order != null)
                await _mailService.SendCompletedOrderMailAsync(order.Email, order.OrderCode, order.OrderDate, order.NameSurname);

            return new();
        }
    }
}
