﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redis.Cache.Core.Services
{
    public class InMemoryCacheService : ICacheService
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public Task<string> GetCacheValueAsync(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            return Task.CompletedTask;
        }
    }
}
