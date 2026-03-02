using Basket.Service.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Service.Concrete
{
    public class RedisService : IRedisService
    {
        //connection to redis
        private readonly IDistributedCache _cache;

        private readonly IRedisService _redisService;
        public RedisService(IDistributedCache cache, IRedisService redisService)
        {
            _cache = cache;
            _redisService = redisService;

        }

        public async Task<string?> GetAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }
    }
}
