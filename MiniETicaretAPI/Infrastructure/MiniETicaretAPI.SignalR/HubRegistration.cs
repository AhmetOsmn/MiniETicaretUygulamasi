using Microsoft.AspNetCore.Builder;
using MiniETicaretAPI.SignalR.Hubs;

namespace MiniETicaretAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>(HubUrls.ProductsHub);
        }
    }
}
