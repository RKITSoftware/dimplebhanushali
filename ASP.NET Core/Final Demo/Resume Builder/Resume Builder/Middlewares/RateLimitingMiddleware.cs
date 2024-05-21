using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// RateLimitingMiddleware For Performing RaeLimiting in Requests.
    /// </summary>
    public class RateLimitingMiddleware
    {
        //#region Private Members

        ///// <summary>
        ///// 
        ///// </summary>
        //private readonly RequestDelegate _next;

        ///// <summary>
        ///// 
        ///// </summary>
        //private readonly IMemoryCache _cache;

        ///// <summary>
        ///// 
        ///// </summary>
        //private readonly ConcurrentQueue<HttpContext> _requestQueue;

        //private readonly string _queueKey = "RateLimitingQueue";


        ///// <summary>
        /////
        ///// </summary>
        //private readonly ILogger<RateLimitingMiddleware> _logger;

        //#endregion

        //#region Constructor

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="next"></param>
        ///// <param name="cache"></param>
        ///// <param name="logger"></param>
        //public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<RateLimitingMiddleware> logger)
        //{
        //    _next = next;
        //    _cache = cache;
        //    _logger = logger;
        //    _requestQueue = new ConcurrentQueue<HttpContext>();
        //}

        //#endregion

        //#region Public Methods

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public async Task Invoke(HttpContext context)
        //{
        //    string ipAddress = context.Connection.RemoteIpAddress.ToString();

        //    // Define the rate limit settings (requests per minute)
        //    int limit = 500;
        //    int expireInMinutes = 1;

        //    string cacheKey = $"{ipAddress}";

        //    if (!_cache.TryGetValue(cacheKey, out int? requestCount))
        //    {
        //        requestCount = 0;
        //    }

        //    requestCount++;

        //    _cache.Set(cacheKey, requestCount, TimeSpan.FromMinutes(expireInMinutes));

        //    if (requestCount > limit)
        //    {
        //        _requestQueue.Enqueue(context);
        //        _logger.LogInformation($"Rate limit exceeded for IP address: {ipAddress}");
        //        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        //        await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
        //        return;
        //    }

        //    await _next(context);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public async Task ProcessQueue()
        //{
        //    while (_requestQueue.TryDequeue(out var context))
        //    {
        //        await _next(context);
        //    }
        //}

        //#endregion

        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly string _queueKey = "RateLimitingQueue";

        public RateLimitingMiddleware(RequestDelegate next, 
                                      IMemoryCache cache, 
                                      ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString();
            int limit = 2;
            int expireInMinutes = 1;
            string cacheKey = $"{ipAddress}";

            if (!_cache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
            }

            requestCount++;
            _cache.Set(cacheKey, requestCount, TimeSpan.FromMinutes(expireInMinutes));

            if (requestCount > limit)
            {
                if (!_cache.TryGetValue(_queueKey, out ConcurrentQueue<HttpContext> requestQueue))
                {
                    requestQueue = new ConcurrentQueue<HttpContext>();
                    _cache.Set(_queueKey, requestQueue);
                }

                context.Items["RequestDelegate"] = _next;
                requestQueue.Enqueue(context);
                _logger.LogInformation($"Rate limit exceeded for IP address: {ipAddress}");
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                return;
            }

            await _next(context);
        }
    }
     
    /// <summary>
    /// Extension Method to add Middleware to builder Services.
    /// </summary>
    public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimitingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}
