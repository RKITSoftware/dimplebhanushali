namespace Certificate_Generator.Middleware
{
    public class RequestProcessingService : BackgroundService
    {
        private readonly RateLimitingMiddleware _rateLimitingMiddleware;

        public RequestProcessingService(RateLimitingMiddleware rateLimitingMiddleware)
        {
            _rateLimitingMiddleware = rateLimitingMiddleware;
        }

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
