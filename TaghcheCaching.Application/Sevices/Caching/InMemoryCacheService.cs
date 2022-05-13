using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interface.Caching;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class InMemoryCacheService : IInMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public object? GetFromMemory(int id)
        {
            if (_memoryCache.TryGetValue(id.ToString(), out object value))
                return value;
            else
                return null;
        }

        public void SetInMemory(int id, object book)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20),
            };
            _memoryCache.Set(id.ToString(), book, cacheExpiryOptions);
        }
    }
}
