using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class MemoryManager : IGetBookCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        public IGetBookCacheManager? NextManager { get ; set ; }

        public MemoryManager(IMemoryCache memoryCache, IGetBookCacheManager bookCacheManager)
        {
            _memoryCache = memoryCache;
            NextManager = bookCacheManager;
        }

        public async Task<BookResponseModel> GetBook(string id)

        {
            if (_memoryCache.TryGetValue(id.ToString(), out object value))
            {
                return new BookResponseModel { Data = value, Message = "Read From Memory"};
            }
            else if (NextManager != null)
            {
                var bookModel = await NextManager.GetBook(id);
                if (bookModel.Data != null)
                {
                    SetBook(id, bookModel.Data);
                }
                return bookModel;
            }
            else
            {
                return null;
            }
        }

        public void SetBook(string id, object? value)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20),
            };
            _memoryCache.Set(id.ToString(), value, cacheExpiryOptions);
        }
    }
}
