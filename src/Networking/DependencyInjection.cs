using Microsoft.Extensions.DependencyInjection;
using Networking.Services.Implementations;
using Networking.Services.Interfaces;

namespace Networking
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNetworkingLayer(this IServiceCollection services)
        {
            services.AddTransient<INetworkAccessor, NetworkAccessor>();
            services.AddTransient<INetworkDataReciever, NetworkDataReciever>();
            services.AddTransient<INetworkDataSender, NetworkDataSender>();

            return services;
        }
    }
}
