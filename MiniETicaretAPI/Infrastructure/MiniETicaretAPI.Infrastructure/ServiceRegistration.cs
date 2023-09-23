using Microsoft.Extensions.DependencyInjection;
using MiniETicaretAPI.Application.Abstactions.Storage;
using MiniETicaretAPI.Infrastructure.Enums;
using MiniETicaretAPI.Infrastructure.Services.Storage;
using MiniETicaretAPI.Infrastructure.Services.Storage.AWS;
using MiniETicaretAPI.Infrastructure.Services.Storage.Azure;
using MiniETicaretAPI.Infrastructure.Services.Storage.Local;

namespace MiniETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T: Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    serviceCollection.AddScoped<IStorage, AwsStorage>();
                    break;                    
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
