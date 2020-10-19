using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis.Cache.Core.Services;

namespace Redis.Cache.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly IDistributedCache _distributedCache;
        public CacheController(ICacheService cacheService, IDistributedCache distributedCache)
        {
            _cacheService = cacheService;
            _distributedCache = distributedCache;
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


        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            List<CacheModel> customerList = new List<CacheModel>();
            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<CacheModel>>(serializedCustomerList);
            }
            else
            {
                CacheModel model = new CacheModel
                {
                    Key = "Name",
                    Value = "Awais"
                };
                customerList.Add(model);


                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }
    }

    public class CacheModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
