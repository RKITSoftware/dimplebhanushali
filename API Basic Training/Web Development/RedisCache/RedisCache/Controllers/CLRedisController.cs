using Caching.BL;
using RedisCache.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedisCache.Controllers
{
    public class CLRedisController : ApiController
    {
        // Instance of BLCaching
        private readonly BLRedis _objBlRedis;

        /// <summary>
        /// initialized instance of BLRedis class
        /// </summary>
        public CLRedisController()
        {
            _objBlRedis = new BLRedis();
        }

        /// <summary>
        /// Retrieve password from username | Credential is stored in cache
        /// </summary>
        /// <param name="e01f01"> username </param>
        /// <returns> password </returns>
        [HttpGet]
        [Route("api/redis/get/{e01f01}")]
        public HttpResponseMessage GetCache(string e01f01)
        {
            var cachedValue = _objBlRedis.RetrieveFromCache(e01f01);

            if (cachedValue != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cachedValue);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"Credential with username '{e01f01}' not found in cache.");
        }

        /// <summary>
        /// Add credential in cache
        /// </summary>
        /// <param name="e01f01"> username </param>
        /// <param name="e01f02"> password </param>
        /// <returns> added message </returns>
        [HttpPost]
        [Route("api/redis/add")]
        public HttpResponseMessage PostCache([FromBody] Cre01 objCre02)
        {
            _objBlRedis.AddToCache(objCre02.e01f01, objCre02.e01f02);

            return Request.CreateResponse(HttpStatusCode.OK, $"Login with username '{objCre02.e01f01}' added to cache.");
        }

        /// <summary>
        /// Update cache value.
        /// </summary>
        /// <param name="e01f01"> username </param>
        /// <param name="e01f02"> password </param>
        /// <returns> updated message </returns>
        [HttpPut]
        [Route("api/redis/update")]
        public HttpResponseMessage UpdateCache([FromBody] Cre01 objCre02)
        {
            var existingValue = _objBlRedis.RetrieveFromCache(objCre02.e01f01);

            if (existingValue != null)
            {
                _objBlRedis.UpdateCache(objCre02.e01f01, objCre02.e01f02);
                return Request.CreateResponse(HttpStatusCode.OK, $"Login with username '{objCre02.e01f01}' updated in cache.");
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"Login with username '{objCre02.e01f01} ' not found in cache.");
        }

        /// <summary>
        /// To delete cache
        /// </summary>
        /// <param name="e01f01"> username </param>
        /// <returns> deleted message </returns>
        [HttpDelete]
        [Route("api/redis/delete/{e01f01}")]
        public HttpResponseMessage DeleteCache(string e01f01)
        {
            var existingValue = _objBlRedis.RetrieveFromCache(e01f01);

            if (existingValue != null)
            {
                _objBlRedis.RemoveFromCache(e01f01);
                return Request.CreateResponse(HttpStatusCode.OK, $"Login with username '{e01f01}' removed from cache.");
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"Login with username '{e01f01}' not found in cache.");
        }

    }
}
