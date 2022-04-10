using Microsoft.Extensions.DependencyInjection;
using MiniETicaretAPI.Application.Abstractions;
using MiniETicaretAPI.Persistence.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaretAPI.Persistence
{
    public static class ServisRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // IProductService tetiklenirse, ProductService calistirilacak.
            services.AddSingleton<IProductService, ProductService>();
        }
    }
}
