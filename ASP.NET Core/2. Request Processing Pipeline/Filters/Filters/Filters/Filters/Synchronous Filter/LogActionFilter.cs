using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters.Synchronous_Filter
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Executing action...");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Action executed.");
        }
    }
}
