using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// Background service for processing requests.
    /// </summary>
    public class RequestProcessingService : BackgroundService
    {
        //private readonly RateLimitingMiddleware _rateLimitingMiddleware;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="RequestProcessingService"/> class.
        ///// </summary>
        ///// <param name="rateLimitingMiddleware">The rate limiting middleware.</param>
        //public RequestProcessingService(RateLimitingMiddleware rateLimitingMiddleware)
        //{
        //    _rateLimitingMiddleware = rateLimitingMiddleware;
        //}

        ///// <summary>
        ///// Executes the background processing logic asynchronously.
        ///// </summary>
        ///// <param name="stoppingToken">The cancellation token that indicates when the background service is stopping.</param>
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        // Dequeue and process requests from the queue
        //        await _rateLimitingMiddleware.ProcessQueue();

        //        // Adjust the frequency of checking the queue as needed
        //        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        //    }
        //}

        private readonly IMemoryCache _cache;
        private readonly ILogger<RequestProcessingService> _logger;
        private readonly string _queueKey = "RateLimitingQueue";

        public RequestProcessingService(IMemoryCache cache, 
                                        ILogger<RequestProcessingService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Rate Limiting Queue Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_cache.TryGetValue(_queueKey, out ConcurrentQueue<HttpContext> requestQueue))
                {
                    while (requestQueue.TryDequeue(out var context))
                    {
                        await ProcessRequest(context);
                    }
                }
                await Task.Delay(1000, stoppingToken); // Adjust the delay as necessary
            }
            _logger.LogInformation("Rate Limiting Queue Service is stopping.");
        }

        private async Task ProcessRequest(HttpContext context)
        {
            var requestDelegate = (RequestDelegate)context.Items["RequestDelegate"];
            await requestDelegate(context);
        }
    }
}
