using Microsoft.Extensions.DependencyInjection;
using MiniETicaretAPI.Application.Abstactions.Hubs;
using MiniETicaretAPI.SignalR.HubServices;

namespace MiniETicaretAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddSignalR();
        }
    }
}
