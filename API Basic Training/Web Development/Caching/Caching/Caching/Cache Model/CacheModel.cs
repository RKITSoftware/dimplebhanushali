using System;
using System.Web;
using System.Web.Caching;

namespace Caching
{
    /// <summary>
    /// Provides methods for caching data in the application.
    /// </summary>
    public class CacheModel
    {
        private static Cache _cache = null;

        private static Cache CacheInstance
        {
            get
            {
                if (_cache == null)
                {
                    // Use HttpContext.Current.Cache if available, otherwise fallback to HttpRuntime.Cache
                    _cache = HttpContext.Current?.Cache ?? HttpRuntime.Cache;
                }
                return _cache;
            }
        }

        /// <summary>
        /// Retrieves an item from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the cached item.</param>
        /// <returns>The cached item, or null if not found.</returns>
        public static object Get(string key)
        {
            return CacheInstance.Get(key);
        }

        /// <summary>
        /// Adds an item to the cache with the specified key and value.
        /// </summary>
        /// <param name="key">The key to associate with the cached item.</param>
        /// <param name="value">The value to be cached.</param>
        public static void Add(string key, object value)
        {
            // You might want to consider using more advanced cache options (e.g., expiration, priority).
            CacheInstance.Insert(key, value);
        }

        /// <summary>
        /// Removes an item from the cache based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the item to be removed from the cache.</param>
        public static void Remove(string key)
        {
            CacheInstance.Remove(key);
        }
    }
}
