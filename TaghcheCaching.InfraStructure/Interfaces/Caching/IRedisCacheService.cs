using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaghcheCaching.InfraStructure.Interface.Caching
{
    public interface IRedisCacheService
    {
        public Task SetInMemory(int id, object book);
        public Task<object?> GetFromRedis(int id);

    }
}
