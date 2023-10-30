﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MiniETicaretAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddMediatR(typeof(ServiceRegistration));

            collection.AddHttpClient();
        }
    }
}
