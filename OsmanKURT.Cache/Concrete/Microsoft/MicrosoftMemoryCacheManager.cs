using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;

namespace OsmanKURT.Cache.Concrete.Microsoft
{
    public class MicrosoftMemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;
        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public void Add(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)
            };

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = Cache.Where(x => regex.IsMatch(x.Key)).Select(x => x.Key).ToList();
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
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
                    Cache.Add(key, result, DateTimeOffset.Now.AddMinutes(duration));

                    return result;
                }
            }
            return (T)cacheItem;
        }
    }
}
