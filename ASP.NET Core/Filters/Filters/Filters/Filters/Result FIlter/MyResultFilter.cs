using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters.Result_FIlter
{
    public class MyResultFilter : IResultFilter
    {
        private readonly ILogger<MyResultFilter> _logger;

        public MyResultFilter(ILogger<MyResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // This code runs before the result is executed
            _logger.LogInformation("Result filter executing");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // This code runs after the result has been executed
            _logger.LogInformation("Result filter executed");
        }
    }
}
