using Microsoft.AspNetCore.SignalR;
using MiniETicaretAPI.Application.Abstactions.Hubs;
using MiniETicaretAPI.SignalR.Hubs;

namespace MiniETicaretAPI.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        private readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
