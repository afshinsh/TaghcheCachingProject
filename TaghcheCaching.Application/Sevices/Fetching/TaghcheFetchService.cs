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
        private readonly string FETCH_BOOK_URL = "https://get.taaghche.com/v2/book/{0}";
        public TaghcheFetchService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<object> FetchBook(int id)
        {
            var url = string.Format(FETCH_BOOK_URL, id);
            var response = await _httpService.GetAsync<object>(url);
            return response;
        }
    }
}
