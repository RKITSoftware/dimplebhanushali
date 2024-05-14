namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// Background service for processing requests.
    /// </summary>
    public class RequestProcessingService : BackgroundService
    {
        private readonly RateLimitingMiddleware _rateLimitingMiddleware;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestProcessingService"/> class.
        /// </summary>
        /// <param name="rateLimitingMiddleware">The rate limiting middleware.</param>
        public RequestProcessingService(RateLimitingMiddleware rateLimitingMiddleware)
        {
            _rateLimitingMiddleware = rateLimitingMiddleware;
        }

        /// <summary>
        /// Executes the background processing logic asynchronously.
        /// </summary>
        /// <param name="stoppingToken">The cancellation token that indicates when the background service is stopping.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Dequeue and process requests from the queue
                await _rateLimitingMiddleware.ProcessQueue();

                // Adjust the frequency of checking the queue as needed
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
