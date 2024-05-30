using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Filters.Filters.Asynchronous_Filter
{
    public class LogAsyncActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogAsyncActionFilter> _logger;

        public LogAsyncActionFilter(ILogger<LogAsyncActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Code to execute before the action method
            _logger.LogInformation("Executing action asynchronously...");

            // Call the action method
            var resultContext = await next();

            // Code to execute after the action method
            _logger.LogInformation("Action executed asynchronously.");
        }
    }
}
