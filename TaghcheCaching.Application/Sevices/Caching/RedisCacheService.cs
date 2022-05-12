using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interface.Caching;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCach;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCach = distributedCache;
        }
    }
}
