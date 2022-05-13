using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.Application.Models;
using TaghcheCaching.Application.Sevices.Caching;
using TaghcheCaching.InfraStructure.Interface.Caching;
using TaghcheCaching.InfraStructure.Interface.Fetching;

namespace TaghcheCaching.Application.Manager
{
    public class CachingManager
    {
        private readonly IInMemoryCacheService _inMemoryCacheService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly ITaghcheFetchService _taghcheFetchService;

        public CachingManager(IInMemoryCacheService inMemoryCacheService, IRedisCacheService redisCacheService,
            ITaghcheFetchService taghcheFetchService)
        {
            _inMemoryCacheService = inMemoryCacheService;
            _redisCacheService = redisCacheService;
            _taghcheFetchService = taghcheFetchService;
        }

        public async Task<ResponseModel> PrepareBook(int id)
        {

            object? book = _inMemoryCacheService.GetFromMemory(id);
            if(book is not null)
                return new ResponseModel { Data = book, Success = true, Message = "Read From Memory!" };
            book = await _redisCacheService.GetFromRedis(id);
            if (book is not null)
                return new ResponseModel { Data = book, Success = true, Message = "Read From Redis!" };
            book = await _taghcheFetchService.FetchBook(id);
            if(book is null)
                return new ResponseModel { Success = false, Message = "Book Not Fount!" };

            _inMemoryCacheService.SetInMemory(id, book);
            await _redisCacheService.SetInMemory(id, book);
            return new ResponseModel { Data = book, Success = true, Message = "Read From Api!" };

        }
    }
}
