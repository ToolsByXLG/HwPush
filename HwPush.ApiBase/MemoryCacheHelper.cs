using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace HwPush.ApiBase
{


    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly Object _locker = new object();
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>缓存值</returns>
        public static T GetCache<T>(String key)
        {
            if (MemoryCache.Default[key] != null)
            {
                return (T)MemoryCache.Default[key];
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取缓存字符串
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>缓存字符串</returns>
        public static string GetCache(String key)
        {
            return (string)MemoryCache.Default[key];
        }



        public static T SetCache<T>(String key, Func<T> cachePopulate)
        {
            MemoryCache.Default[key] = null;
            return GetOrSetCache<T>(key, cachePopulate);
        }
        public static T SetCache<T>(String key, T cachePopulate)
        {
            return SetCache<T>(key, delegate () { return cachePopulate; });
        }
        public static string SetCache(String key, string cachePopulate)
        {
            return SetCache<string>(key, cachePopulate);
        }


        public static T GetOrSetCache<T>(String key, Func<T> cachePopulate)
        {
            TimeSpan slidingExpiration = new TimeSpan(0, 10, 0);
            return GetCacheItem<T>(key, cachePopulate, slidingExpiration);
        }
        public static T GetOrSetCache<T>(String key, T cachePopulate)
        {
            return GetOrSetCache<T>(key, delegate () { return cachePopulate; });
        }
        public static string GetOrSetCache(String key, string cachePopulate)
        {
            return GetOrSetCache<string>(key,  cachePopulate);
        }


        public static T GetCacheItem<T>(String key, T cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            return GetCacheItem<T>(key, delegate () { return cachePopulate; }, slidingExpiration, absoluteExpiration);
        }
        public static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
    }



}