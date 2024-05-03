using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Certificate_Generator.Middleware
{
    /// <summary>
    /// RateLimitingMiddleware For Performing RaeLimiting in Requests.
    /// </summary>
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ConcurrentQueue<HttpContext> _requestQueue;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
            _requestQueue = new ConcurrentQueue<HttpContext>();
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            // Define the rate limit settings (requests per minute)
            var limit = 500;
            var expireInMinutes = 1;

            var cacheKey = $"{ipAddress}";

            if (!_cache.TryGetValue(cacheKey, out int? requestCount))
            {
                requestCount = 0;
            }

            requestCount++;

            _cache.Set(cacheKey, requestCount, TimeSpan.FromMinutes(expireInMinutes));

            if (requestCount > limit)
            {
                _requestQueue.Enqueue(context);
                _logger.LogInformation($"Rate limit exceeded for IP address: {ipAddress}");
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                return;
            }

            await _next(context);
        }

        public async Task ProcessQueue()
        {
            while (_requestQueue.TryDequeue(out var context))
            {
                await _next(context);
            }
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
