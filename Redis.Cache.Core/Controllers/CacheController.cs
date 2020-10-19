using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redis.Cache.Core.Services;

namespace Redis.Cache.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        
        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult)NotFound() : Ok(value);
        }

        [HttpPost("set")]
        public async Task<IActionResult> Set([FromBody] CacheModel model)
        {
            await _cacheService.SetCacheValueAsync(model.Key, model.Value);
            return Ok();
        }
    }

    public class CacheModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
