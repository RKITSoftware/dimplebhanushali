using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// Background service for processing requests.
    /// </summary>
    public class RequestProcessingService : BackgroundService
    {
        #region Private Members
        /// <summary>
        /// Represents the memory cache service used for storing request counts.
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Represents the logger service used for logging errors and information.
        /// </summary>
        private readonly ILogger<RequestProcessingService> _logger;

        /// <summary>
        /// Represents the queue of HttpContext objects representing requests to be processed.
        /// </summary>
        private readonly ConcurrentQueue<HttpContext> _requestQueue;

        /// <summary>
        /// Represents a semaphore used for controlling access to the request queue.
        /// </summary>
        private readonly SemaphoreSlim _queueSemaphore = new SemaphoreSlim(1);

        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestProcessingService"/> class.
        /// </summary>
        /// <param name="cache">The memory cache service.</param>
        /// <param name="logger">The logger service.</param>
        public RequestProcessingService(IMemoryCache cache, ILogger<RequestProcessingService> logger)
        {
            _cache = cache;
            _logger = logger;
            _requestQueue = new ConcurrentQueue<HttpContext>();
        }

        /// <summary>
        /// Enqueues a request to be processed.
        /// </summary>
        /// <param name="context">The HttpContext representing the request to be processed.</param>
        public void EnqueueRequest(HttpContext context)
        {
            _requestQueue.Enqueue(context);
        }

        #endregion

        #region Protected Method
        /// <summary>
        /// Executes the background processing logic asynchronously.
        /// </summary>
        /// <param name="stoppingToken">The cancellation token that indicates when the background service is stopping.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1000), stoppingToken); // Process the queue every 10 seconds

                while (_requestQueue.TryDequeue(out var context))
                {
                    try
                    {
                        await _queueSemaphore.WaitAsync(stoppingToken);

                        string ipAddress = context.Connection.RemoteIpAddress.ToString();
                        string cacheKey = $"{ipAddress}";

                        if (_cache.TryGetValue(cacheKey, out int requestCount) && requestCount <= 2)
                        {
                            await context.RequestServices.GetRequiredService<RequestDelegate>().Invoke(context);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing request from the queue.");
                    }
                    finally
                    {
                        _queueSemaphore.Release();
                    }
                }
            }
        }
        #endregion
    }
}
