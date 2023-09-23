using Microsoft.Extensions.DependencyInjection;
using MiniETicaretAPI.Application.Services;
using MiniETicaretAPI.Infrastructure.Services;

namespace MiniETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
        }
    }
}
