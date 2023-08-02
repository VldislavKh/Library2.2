using Domain.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Library2._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedisController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationContext _context;

        public RedisController(IDistributedCache cache, ApplicationContext context)
        {
            _cache = cache;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<string> GetCache(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SetCache(string value)
        {
            var guid = Guid.NewGuid();
            await _cache.SetStringAsync(guid.ToString(), value);
            return Ok(new { guid });
        }
    }
}
