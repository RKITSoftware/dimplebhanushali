using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action executing: {context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action executed: {context.ActionDescriptor.DisplayName}");
        }
    }
}
