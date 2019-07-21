using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Cache
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Add(string key, object data, int cacheTime);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
        T ExecuteCached<T>(string key, int duration, Func<T> codetoExecute);
    }
}
