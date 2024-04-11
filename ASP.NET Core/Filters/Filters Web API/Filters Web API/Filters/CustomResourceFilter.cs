using Filters_Web_API.MAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Filters_Web_API.Filters
{
    public class CustomResourceFilter : IResourceFilter
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "BooksCacheKey";

        public CustomResourceFilter()
        {
            _logger = new LoggerFactory().CreateLogger<CustomResourceFilter>();
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // Check if data is available in cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Book> cachedBooks))
            {
                // If data is found in cache, break the filter chain
                context.Result = new ObjectResult(cachedBooks)
                {
                    StatusCode = 200
                };

                // Log the message indicating data retrieved from cache
                _logger.LogInformation("Data retrieved from cache. Filter chain is broken.");
                return;
            }

            // If data is not found in cache, continue with the filter chain
            _logger.LogInformation($"Resource executing for: {context.ActionDescriptor.DisplayName}");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // No action needed
        }
    }
}
