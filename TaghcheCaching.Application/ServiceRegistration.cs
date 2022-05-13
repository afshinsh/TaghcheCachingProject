using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaghcheCaching.Application.Manager;
using TaghcheCaching.Application.Sevices.Caching;
using TaghcheCaching.Application.Sevices.Fetching;
using TaghcheCaching.InfraStructure;
using TaghcheCaching.InfraStructure.Interface.Caching;
using TaghcheCaching.InfraStructure.Interface.Fetching;

namespace TaghcheCaching.Application
{
    public static class ServiceRegistration
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddStackExchangeRedisCache(o => o.Configuration = configuration["Redis"]);
            services.AddScoped<IInMemoryCacheService, InMemoryCacheService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped<ITaghcheFetchService, TaghcheFetchService>();
            services.AddScoped<CachingManager>();
            services.AddScoped<HttpService>();

        }
    }
}