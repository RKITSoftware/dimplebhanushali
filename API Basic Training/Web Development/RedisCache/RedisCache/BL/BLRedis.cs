using StackExchange.Redis;

namespace Caching.BL
{
    /// <summary>
    /// Actual Implementation of CRUD operations with Redis
    /// </summary>
    public class BLRedis
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        /// <summary>
        /// Connection Establishment with Redis 
        /// </summary>
        public BLRedis()
        {
            // Initialize the ConnectionMultiplexer in the constructor

            // At port : 6378
            _redis = ConnectionMultiplexer.Connect("localhost:6379");

            // Cloud Redis
            //_redis = ConnectionMultiplexer.Connect("redis-12113.c330.asia-south1-1.gce.redns.redis-cloud.com:12113,password=p3aj2bNADNozY1bW1y2k4Un3YKxX1Gjc");
            _db = _redis.GetDatabase();
        }

        /// <summary>
        /// Add value to the Redis cache
        /// </summary>
        /// <param name="key">Key for the cache entry</param>
        /// <param name="value">Value to be stored in the cache</param>
        public void AddToCache(string key, string value)
        {
            _db.StringSet(key, value);
        }

        /// <summary>
        /// Retrieve value from the Redis cache
        /// </summary>
        /// <param name="key">Key to retrieve the value</param>
        /// <returns>Value from the cache, or null if not found</returns>
        public string RetrieveFromCache(string key)
        {
            return _db.StringGet(key);
        }

        /// <summary>
        /// Update value in the Redis cache
        /// </summary>
        /// <param name="key">Key for the cache entry</param>
        /// <param name="newValue">New value to replace the existing value</param>
        public void UpdateCache(string key, string newValue)
        {
            if (_db.KeyExists(key))
            {
                _db.StringSet(key, newValue);
            }
        }

        /// <summary>
        /// Remove value from the Redis cache
        /// </summary>
        /// <param name="key">Key for the cache entry to be removed</param>
        public void RemoveFromCache(string key)
        {
            _db.KeyDelete(key);
        }
    }
}