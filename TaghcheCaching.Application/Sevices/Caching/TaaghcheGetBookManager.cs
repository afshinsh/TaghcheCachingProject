using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;
using TaghcheCaching.InfraStructure.Utilities;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public class TaaghcheGetBookManager : GetBookCacheManager
    {

        private readonly HttpService _httpService;
        private readonly string FETCH_BOOK_URL = "https://get.taaghche.com/v2/book/{0}";

        public TaaghcheGetBookManager(HttpService httpService)
        {
            _httpService = httpService;
        }

        public override async Task<BookResponseModel> GetBook(string id)
        {
            var url = string.Format(FETCH_BOOK_URL, id);
            var response = await _httpService.GetAsync<object>(url);
            return new BookResponseModel 
            { 
                Data = response, 
                Message = "Read From Taaghche" 
            };
        }

        public override void SetBook(string id, object? value)
        {
            return;
        }
    }
}
