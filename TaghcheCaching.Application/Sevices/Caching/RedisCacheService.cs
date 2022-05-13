using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interface.Caching;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<object?> GetFromRedis(int id)
        {
            var book = await _distributedCache.GetAsync(id.ToString());
            if (book == null)
                return null;
            else
            {
                var bookString = Encoding.ASCII.GetString(book);
                return bookString.FromJson<object>();
            }
                
        }
        public async Task SetInMemory(int id, object book)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2)).SetSlidingExpiration(TimeSpan.FromMinutes(1));

            await _distributedCache.SetAsync(id.ToString(), Encoding.ASCII.GetBytes(book.ToJson()), options);

        }
    }
}
