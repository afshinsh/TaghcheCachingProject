using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class RedisGetBookCacheManager : GetBookCacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisGetBookCacheManager(IDistributedCache distributedCache, IGetBookCacheManager bookCacheManager)
        {
            _distributedCache = distributedCache;
            NextManager = bookCacheManager;
        }

        public override async Task<BookResponseModel> GetBook(string id)
        {
            var book = await _distributedCache.GetAsync(id.ToString());
            if (book != null)
            {
                var bookString = Encoding.ASCII.GetString(book);
                return new BookResponseModel 
                { 
                    Data = bookString.FromJson<object>(),
                    Message = "Read From Redis" 
                };
            }
            return await GetAndSetBook(id);
        }

        public override void SetBook(string id, object? value)
        {
            var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(1));
            if(value != null)
            {
                _distributedCache.SetAsync(id.ToString(), Encoding.ASCII.GetBytes(value.ToJson()), options);
            }
        }
    }
}
