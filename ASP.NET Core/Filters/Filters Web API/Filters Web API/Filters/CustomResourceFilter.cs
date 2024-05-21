using Filters_Web_API.MAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom resource filter to manage caching of books data.
    /// </summary>
    public class CustomResourceFilter : IResourceFilter
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "BooksCacheKey";

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResourceFilter"/> class.
        /// </summary>
        public CustomResourceFilter()
        {
            _logger = new LoggerFactory().CreateLogger<CustomResourceFilter>();
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// Method called before the resource is executed.
        /// </summary>
        /// <param name="context">The context of the resource executing.</param>
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

        /// <summary>
        /// Method called after the resource is executed.
        /// </summary>
        /// <param name="context">The context of the resource executed.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // No action needed
        }
    }
}
