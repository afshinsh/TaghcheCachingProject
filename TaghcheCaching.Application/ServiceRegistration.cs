using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaghcheCaching.Application.Manager;
using TaghcheCaching.Application.Sevices.Caching;
using TaghcheCaching.InfraStructure;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application
{
    public static class ServiceRegistration
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddStackExchangeRedisCache(o => o.Configuration = configuration["Redis"]);  
            services.AddScoped<HttpService>();
            services.AddScoped<IManager, ManagerService>();
            services.AddScoped<MemoryManager>();
            services.AddScoped<RedisManager>();
            services.AddScoped<TaaghcheFetchManager>();

            services.AddScoped<CachingManager>();

        }
    }
}