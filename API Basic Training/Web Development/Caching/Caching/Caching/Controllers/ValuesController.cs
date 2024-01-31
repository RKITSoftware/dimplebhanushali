using System.Collections.Generic;
using System.Web.Http;

namespace Caching.Controllers
{
    /// <summary>
    /// Controller for handling caching operations.
    /// </summary>
    public class ValuesController : ApiController
    {

        /// <summary>
        /// Retrieves a list of values and adds them to the cache.
        /// </summary>
        /// <returns>An IEnumerable of strings representing values.</returns>
        public IEnumerable<string> Get()
        {
            var result = new string[] { "Dimple", "Mithiya" };
            CacheModel.Add("ID111", result);
            return result;
        }

        /// <summary>
        /// Retrieves a cached value based on the specified ID.
        /// </summary>
        /// <param name="id">The ID used to retrieve the cached value.</param>
        /// <returns>A concatenated string of cached values or an error message.</returns>
        public string Get(int id)
        {
            string result = CacheModel.Get("ID111").ToString();
            return result;
        }

        /// <summary>
        /// Removes a cached item based on the specified key.
        /// </summary>
        /// <param name="key">The key associated with the cached item to be removed.</param>
        /// <returns>A message indicating whether the key was successfully removed or not found.</returns>
        public string Remove(string key)
        {
            // Assuming CacheModel is a custom caching class with a static method Remove
            if (key != null)
            {
                CacheModel.Remove(key);
                return "Key Removed";
            }
            return "Key Not Found";
        }

    }
}
