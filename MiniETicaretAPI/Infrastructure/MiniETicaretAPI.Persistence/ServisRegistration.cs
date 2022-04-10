using Microsoft.Extensions.DependencyInjection;
using MiniETicaretAPI.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using MiniETicaretAPI.Persistence.Contexts;
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
            services.AddDbContext<MiniETicaretAPIDbContext>(options => 
                options.UseNpgsql(Configuration.ConnectionString));
        }
    }
}
