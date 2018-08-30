using System;
using System.Web.Caching;
using System.Configuration;
using System.Text;
using System.Web;

namespace ZLERP.Business
{
    /// <summary>
    /// Cache Help class
    /// </summary>
    public class CacheHelper
    {
        private static int m_CacheTimeoutMinutes = ConfigurationManager.AppSettings["Cache_TimeoutMinutes"]==null ? 0 :  Convert.ToInt32(ConfigurationManager.AppSettings["Cache_TimeoutMinutes"]);
        public delegate TT GetCacheItemDelegate<TT>();
        /// <summary>
        /// 取得缓存的对象
        /// </summary>
        /// <typeparam name="TT">对象类型</typeparam>
        /// <param name="cacheKey">缓存key</param>
        /// <param name="syncLock">同步锁</param>
        /// <param name="priority">优先级</param>
        /// <param name="refreshAction">刷新时间,一般为null</param>
        /// <param name="cacheDependency">缓存依赖项</param>
        /// <param name="timeout">缓存时间</param>
        /// <param name="getCacheItem">对象没被缓存时获取对象的方法委托</param>
        /// <returns></returns>
        public static TT GetCacheItem<TT>(string cacheKey, object syncLock,
            CacheItemPriority priority, CacheItemRemovedCallback refreshAction,
            CacheDependency cacheDependency, TimeSpan timeout, GetCacheItemDelegate<TT> getCacheItem)
        {
            object result = System.Web.HttpRuntime.Cache.Get(cacheKey);
            if (result == null)
            {
                lock (syncLock)
                {
                    result = System.Web.HttpRuntime.Cache.Get(cacheKey);
                    if (result == null)
                    {
                        result = getCacheItem();
                        if(result!=null)
                            System.Web.HttpRuntime.Cache.Add(cacheKey, result, cacheDependency,
                                DateTime.Now.Add(timeout), TimeSpan.Zero, priority, refreshAction);
                    }
                    
                }
            }
            return (TT)result;
        }

        /// <summary>
        /// 取得缓存的对象
        /// </summary>
        /// <typeparam name="TT">对象类型</typeparam>
        /// <param name="cacheKey">缓存key</param>
        /// <param name="syncLock">同步锁</param>
        /// <param name="priority">优先级</param>
        /// <param name="refreshAction">刷新时间,一般为null</param>
        /// <param name="cacheDependency">缓存依赖项</param>
        /// <param name="timeout">缓存时间</param>
        /// <param name="getCacheItem">对象没被缓存时获取对象的方法委托</param>
        /// <returns></returns>
        public static TT GetCacheItem<TT>(string cacheKey, object syncLock,
            TimeSpan timeout, GetCacheItemDelegate<TT> getCacheItem)
        {
            return GetCacheItem<TT>(cacheKey, syncLock, CacheItemPriority.Normal, null, null, timeout, getCacheItem);
        }

        /// <summary>
        /// 取得缓存的对象,按配置中的Cache_TimeoutMinutes缓存
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="syncLock"></param>
        /// <param name="getCacheItem"></param>
        /// <returns></returns>
        public static TT GetCacheItem<TT>(string cacheKey, object syncLock, GetCacheItemDelegate<TT> getCacheItem)
        {
            return GetCacheItem<TT>(cacheKey, 
                syncLock, 
                CacheItemPriority.Normal,
                null, 
                null, 
                TimeSpan.FromMinutes(m_CacheTimeoutMinutes), 
                getCacheItem);
        }
        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="syncLock"></param>
        /// <param name="cacheDependencyFileName">缓存依赖的文件</param>
        /// <param name="getCacheItem"></param>
        /// <returns></returns>
        public static TT GetCacheItem<TT>(string cacheKey, object syncLock, string cacheDependencyFileName,
            GetCacheItemDelegate<TT> getCacheItem)
        {
            return GetCacheItem<TT>(cacheKey,
                syncLock,
                CacheItemPriority.Normal,
                null,
                new CacheDependency(cacheDependencyFileName),
                TimeSpan.FromHours(1),
                getCacheItem);
        }

        /// <summary>
        /// 返回缓存键标识字符串
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="prefix"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetCacheKey<TEntity>(string prefix, params string[] parameters)
        {
            string cacheKey = "Cache_{0}_{1}_{2}";
            StringBuilder sb = new StringBuilder();
            if (parameters != null) {
                foreach (string s in parameters) {
                    sb.Append(s);
                }
            }
            return string.Format(cacheKey, typeof(TEntity).Name, prefix, sb.ToString().GetHashCode());
        }

        /// <summary>
        /// 清除cache
        /// </summary>
        /// <param name="entityType">指定实体类名则只删除指定类型的Cache,传入null清空所有</param>
        public static void RemoveCacheByType(Type entityType)
        {
            var caches = HttpContext.Current.Cache.GetEnumerator();
            while (caches.MoveNext())
            {
                if (entityType != null && caches.Key.ToString().StartsWith("Cache_" + entityType.Name))
                {
                    HttpContext.Current.Cache.Remove(caches.Key.ToString());
                }
                else
                    HttpContext.Current.Cache.Remove(caches.Key.ToString());
            }
           
        }

        public static void RemoveCache(string cacheKey) {
            HttpContext.Current.Cache.Remove(cacheKey);
        }


    }
}
