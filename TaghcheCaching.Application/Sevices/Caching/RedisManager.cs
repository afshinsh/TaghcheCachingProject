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
    public class RedisManager : IGetBookCacheManager
    {
        private readonly IDistributedCache _distributedCache;
        public IGetBookCacheManager? NextManager { get; set; }

        public RedisManager(IDistributedCache distributedCache, IGetBookCacheManager bookCacheManager)
        {
            _distributedCache = distributedCache;
            NextManager = bookCacheManager;
        }

        public async Task<BookResponseModel> GetBook(string id)
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
