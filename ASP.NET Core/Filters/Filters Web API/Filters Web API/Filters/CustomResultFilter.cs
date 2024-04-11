using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger _logger;

        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation($"Result executed for: {context.ActionDescriptor.DisplayName}");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation($"Result executing for: {context.ActionDescriptor.DisplayName}");
        }
    }
}
