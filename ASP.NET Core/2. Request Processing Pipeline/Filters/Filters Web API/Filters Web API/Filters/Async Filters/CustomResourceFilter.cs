using Filters_Web_API.MAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom resource filter to manage caching of books data.
    /// </summary>
    public class CustomResourceFilter : IAsyncResourceFilter
    {
        #region Private Members
        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<CustomResourceFilter> _logger;

        /// <summary>
        /// Memory Cache
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Cache Key
        /// </summary>
        private const string CacheKey = "BooksCacheKey";
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CustomResourceFilter class.
        /// </summary>
        public CustomResourceFilter(IMemoryCache cache, ILogger<CustomResourceFilter> logger)
        {
            _logger = logger;
            _cache = cache;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method called before and after the resource is executed.
        /// </summary>
        /// <param name="context">The context of the resource executing.</param>
        /// <param name="next">Delegate to execute the next resource filter or action.</param>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // Check if data is available in cache
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Book> cachedBooks))
            {
                // If data is found in cache, set the result and return
                context.Result = new ObjectResult(cachedBooks)
                {
                    StatusCode = 200
                };

                // Log the message indicating data retrieved from cache
                _logger.LogInformation("Data retrieved from cache. Filter chain is broken.");
                return;
            }

            // Log that resource execution will continue
            _logger.LogInformation($"Resource executing for: {context.ActionDescriptor.DisplayName}");

            // Continue with the next filter/action in the pipeline
            var executedContext = await next();

            // After the action has executed, check if the result is of type ObjectResult and contains books
            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IEnumerable<Book> books)
            {
                // Cache the result if it is a list of books
                _cache.Set(CacheKey, books);

                // Log the message indicating data cached successfully
                _logger.LogInformation("Data cached successfully.");
            }
            else
            {
                _logger.LogWarning("Data not cached. The result is not of type IEnumerable<Book>.");
            }
        }
        #endregion
    }
}
