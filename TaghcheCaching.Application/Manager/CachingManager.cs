using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.Application.Sevices.Caching;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Manager
{
    public class CachingManager
    {

        private readonly IGetBookCacheManager _managerService;

        public CachingManager(IMemoryCache memoryCache, 
            IDistributedCache distributedCache,
            HttpService httpService)
        {
            _managerService = new MemoryManager(memoryCache,
                new RedisManager(distributedCache,
                new TaaghcheFetchManager(httpService)));
        }

        public async Task<BookResponseModel> PrepareBook(int id)
        {

            var book = await _managerService.GetBook(id.ToString());
            if (book.Data is null)
                return new BookResponseModel { Success = false, Message = "Book Not Found!" };
            else
                return book;
        }
    }
}
