using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.Application.Sevices.Caching;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;

namespace TaghcheCaching.Application.Manager
{
    public class CachingManager
    {

        private readonly IManager _managerService;

        public CachingManager(MemoryManager inMemoryCacheService, RedisManager redisCacheService,
            TaaghcheFetchManager taaghcheFetchService)
        {
            _managerService = inMemoryCacheService;
            redisCacheService.SetNextManager(taaghcheFetchService);
            _managerService.SetNextManager(redisCacheService);
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
