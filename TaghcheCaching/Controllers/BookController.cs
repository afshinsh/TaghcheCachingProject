using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaghcheCaching.Application.Manager;
using TestNuget;

namespace TaghcheCaching.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly CachingManager _cachingManager;
        public BookController(CachingManager cachingManager)
        {
            _cachingManager = cachingManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            return Ok(await _cachingManager.PrepareBook(id));
        }

    }
}
