using StackExchange.Redis.Extensions.Core;
using System;

namespace OsmanKURT.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly ICacheClient _cache;


        public RedisCacheManager(string ipAddress, int port)
        {
            var redisConfig = new RedisConfigurationManager(ipAddress, port);
            _cache = redisConfig.GetConnection();
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Add(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }

            _cache.Add(key, data, DateTimeOffset.Now.AddMinutes(cacheTime));

        }

        public bool IsAdd(string key)
        {
            return _cache.Exists(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var keys = _cache.SearchKeys(pattern);
            _cache.RemoveAll(keys);
        }

        public void Clear()
        {
            _cache.FlushDb();
        }

        public T ExecuteCached<T>(string key, int duration, Func<T> codetoExecute)
        {
            var cacheItem = Get<T>(key);

            var defaultType = default(T);

            if (cacheItem == null || cacheItem.Equals(defaultType))
            {
                T result = codetoExecute.Invoke();

                if (result != null)
                {
                    _cache.Add(key, result, DateTimeOffset.Now.AddMinutes(duration));

                    return result;
                }
            }
            return (T)cacheItem;
        }
    }
}
