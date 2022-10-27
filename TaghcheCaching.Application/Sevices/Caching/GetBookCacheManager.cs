using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public abstract class GetBookCacheManager : IGetBookCacheManager
    {
        public IGetBookCacheManager? NextManager { get; set; }
        public abstract Task<BookResponseModel> GetBook(string id);
        public abstract void SetBook(string id, object? value);

        protected async Task<BookResponseModel> GetAndSetBook(string id)
        {
            if (NextManager != null)
            {
                var bookModel = await NextManager.GetBook(id);
                if (bookModel.Data != null)
                {
                    SetBook(id, bookModel.Data);
                }
                return bookModel;
            }
            return null;
        }
    }
}
