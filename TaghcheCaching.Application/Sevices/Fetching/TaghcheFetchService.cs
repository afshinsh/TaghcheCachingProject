using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure;
using TaghcheCaching.InfraStructure.Interface.Fetching;

namespace TaghcheCaching.Application.Sevices.Fetching
{
    public class TaghcheFetchService : ITaghcheFetchService
    {
        private readonly HttpService _httpService;
        public TaghcheFetchService(HttpService httpService)
        {
            _httpService = httpService;
        }
    }
}
