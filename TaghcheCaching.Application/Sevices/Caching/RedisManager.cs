using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Models;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class RedisManager : ManagerService
    {
        private readonly IDistributedCache _distributedCache;


        public RedisManager([FromServices] IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public override async Task<BookResponseModel> GetBook(string id)
        {
            var book = await _distributedCache.GetAsync(id.ToString());
            if (book is not null)
            {
                var bookString = Encoding.ASCII.GetString(book);
                return new BookResponseModel { Data = bookString.FromJson<object>(), Message = "Read From Redis" };
            }
            else if (NextManager != null)
            {
                var bookModel = await NextManager.GetBook(id);
                if (bookModel.Data is not null)
                    SetBook(id, bookModel.Data);
                return bookModel;
            }
            else
                return null;
        }

        public override void SetBook(string id, object? value)
        {
            var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2)).SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _distributedCache.SetAsync(id.ToString(), Encoding.ASCII.GetBytes(value.ToJson()), options);
        }
    }
}
