using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Marketplace.API.Infrastructure.Cache
{
    public class Cacher : ICacher
    {
        private readonly IMemoryCache _memoryCache;
        private CancellationTokenSource _resetCacheToken = new CancellationTokenSource();

        public Cacher(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string id)
        {
            if (string.IsNullOrEmpty(id)) return default(T);

            string key = IdentityMap.CreateKey(id);

            CacheEntry cacheEntry = (CacheEntry)_memoryCache.Get(key);

            if (cacheEntry != null)
            {
                return (T)cacheEntry.Value;
            }

            return default(T);
        }

        public bool Save<T>(T obj, string id)
        {
            return Save<T>(obj, id, TimeSpan.MaxValue);
        }

        public bool Save<T>(T obj, string id, TimeSpan expiresIn)
        {
            if (obj == null) return false;
            var listKey = IdentityMap.CreateKey(id);

            return CacheSet(listKey, obj, DateTime.Now.Add(expiresIn));
        }

        public bool SaveAll<T>(ICollection<T> list)
        {
            return SaveAll<T>(list, TimeSpan.MaxValue);
        }

        public bool SaveAll<T>(ICollection<T> list, TimeSpan expiresIn, bool expireTimeAsParameter = false)
        {
            string listKey = IdentityMap.CreateKey<T>();
            if (expireTimeAsParameter)
                return CacheSet(listKey, list, expiresIn);
            return CacheSet(listKey, list);
        }

        public T GetOrCreate<T>(string id)
        {
            var value = this.Get<T>(id);
            if (value == default(T))
            {
                Save<T>()
            }
        }

        private bool CacheSet(string key, object value, TimeSpan ts)
        {
            return CacheSet(key, value, DateTime.Now.Add(ts));
        }

        private bool CacheSet(string key, object value)
        {
            var duration = TimeSpan.MaxValue;
            return CacheSet(key, value, DateTime.Now.Add(duration));
        }

        private bool CacheSet(string key, object value, DateTime expiresAt)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.MaxValue)
                .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

            _memoryCache.Set(key, new CacheEntry(value, expiresAt), cacheEntryOptions);
            return true;
        }

        public bool Save(string key, object obj)
        {
            return CacheSet(key, obj);
        }
    }

    public interface ICacher
    {
        T Get<T>(string id);
        bool Save<T>(T obj, string id);
        bool Save<T>(T obj, string id, TimeSpan expiresIn);
        bool SaveAll<T>(ICollection<T> list);
        bool SaveAll<T>(ICollection<T> list, TimeSpan expiresIn, bool expireTimeAsParameter = false);
    }


    public class CacheEntry
    {
        private object _cacheValue;

        public CacheEntry(object value, DateTime expiresAt)
        {
            Value = value;
            ExpiresAt = expiresAt;
            LastModifiedTicks = DateTime.Now.Ticks;
        }

        internal DateTime ExpiresAt { get; set; }

        internal object Value
        {
            get => _cacheValue;
            set
            {
                _cacheValue = value;
                LastModifiedTicks = DateTime.Now.Ticks;
            }
        }

        internal long LastModifiedTicks { get; private set; }
    }

    public static class IdentityMap
    {
        public static string CreateKey<T>()
        {
            string key = typeof(T)?.FullName?.Replace(".", "_");
            return key;
        }

        /* public static string CreateKey<T>(string id)
         {
             string key = typeof(T).FullName.Replace(".", "_");
             key = key + "_" + id;
             return key;
         }*/

        public static string CreateKey(string id)
        {
            string key = id.Replace(".", "_");
            return key;
        }
    }
}
