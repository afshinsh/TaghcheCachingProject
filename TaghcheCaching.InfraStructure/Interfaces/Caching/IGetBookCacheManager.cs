using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Models;

namespace TaghcheCaching.InfraStructure.Interfaces.Caching
{
    public interface IGetBookCacheManager
    {
        public Task<BookResponseModel> GetBook(string id);
        public abstract void SetBook(string id, object? value);
    }
}
