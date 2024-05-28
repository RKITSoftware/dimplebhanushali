using Microsoft.Extensions.Caching.Memory;

namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// RateLimitingMiddleware For Performing RaeLimiting in Requests.
    /// </summary>
    public class RateLimitingMiddleware
    {
        #region Private Members
        /// <summary>
        /// Represents the delegate representing the next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Represents the memory cache service used for rate limiting.
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Represents the logger service used for logging rate limit exceeded events.
        /// </summary>
        private readonly ILogger<RateLimitingMiddleware> _logger;

        /// <summary>
        /// Represents the service provider used for resolving dependencies.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RateLimitingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        /// <param name="cache">The memory cache service used for rate limiting.</param>
        /// <param name="logger">The logger service used for logging rate limit exceeded events.</param>
        /// <param name="serviceProvider">The service provider used for resolving dependencies.</param>
        public RateLimitingMiddleware(RequestDelegate next,
                                      IMemoryCache cache,
                                      ILogger<RateLimitingMiddleware> logger,
                                      IServiceProvider serviceProvider)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Invokes the rate limiting logic for incoming requests.
        /// </summary>
        /// <param name="context">The HttpContext representing the incoming request.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            // Obtain a reference to RateLimitingQueueProcessor from the service provider
            var queueProcessor = _serviceProvider.GetRequiredService<RequestProcessingService>();

            string ipAddress = context.Connection.RemoteIpAddress.ToString();
            int limit = 5000;
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
                queueProcessor.EnqueueRequest(context);
                _logger.LogInformation($"Rate limit exceeded for IP address: {ipAddress}");
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                return;
            }

            await _next(context);
        }
        #endregion
    }

    #region Middleware Extension Class
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
    #endregion
}
