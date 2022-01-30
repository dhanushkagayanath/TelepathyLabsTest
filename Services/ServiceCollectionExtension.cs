using Microsoft.Extensions.DependencyInjection;
using Services.Interface;
using System;

namespace Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessLibrary(this IServiceCollection services)
        {
            // Dependency Injection
            services.AddScoped<IBTService, BTService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddSingleton<IDataService, DataService>();
            return services;
        }

    }
}
