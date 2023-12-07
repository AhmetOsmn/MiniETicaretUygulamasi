using Microsoft.AspNetCore.SignalR;
using MiniETicaretAPI.Application.Abstactions.Hubs;
using MiniETicaretAPI.SignalR.Hubs;

namespace MiniETicaretAPI.SignalR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext;

        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message) => await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.OrderAddedMessage, message);
    }
}
