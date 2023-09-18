using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabase _azureCache;

        public CacheService(IDistributedCache distributedCache, IDatabase azureCache)
        {
            _distributedCache = distributedCache;
            _azureCache = azureCache;
        }
        public async Task<string?> GetStringAsync(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        public async Task SetStringAsync(string key, string? serializedData)
        {
            await _distributedCache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        public async Task<RedisValue> StringGet(string key)
        {
            return await _azureCache.StringGetAsync(key);
        }

        public async Task<RedisValue> StringSet(string key, string serializedData)
        {
            return await _azureCache.StringSetAsync(key, serializedData,new TimeSpan(0,5,0));
        }

        public bool StringDelete(string key)
        {
            if (_azureCache.KeyExists(key))
            {                
                return _azureCache.KeyDelete(key); ;
            }
            return false;
        }

    }
}
