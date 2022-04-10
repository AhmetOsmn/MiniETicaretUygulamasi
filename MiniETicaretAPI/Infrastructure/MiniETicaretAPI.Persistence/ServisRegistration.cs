using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniETicaretAPI.Persistence.Repositories;
using MiniETicaretAPI.Application.Repositories;

namespace MiniETicaretAPI.Persistence
{
    public static class ServisRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<MiniETicaretAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);

            services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
                     
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
