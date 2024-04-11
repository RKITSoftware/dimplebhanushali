using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters.Resource_FIlter
{
    public class MyResourceFilter : IResourceFilter
    {
        private readonly ILogger<MyResourceFilter> _logger;

        public MyResourceFilter(ILogger<MyResourceFilter> logger)
        {
            _logger = logger;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // This code runs before the action method is executed
            _logger.LogInformation("Resource filter executing");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // This code runs after the action method has been executed
            _logger.LogInformation("Resource filter executed");
        }
    }
}
