using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Filters.Exception_Filter
{
    public class MyExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<MyExceptionFilter> _logger;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // This code runs when an exception occurs
            _logger.LogError(context.Exception, "An exception occurred");

            // Handle the exception or perform custom error handling logic
            context.Result = new StatusCodeResult(500); // Return a 500 Internal Server Error response
        }
    }
}
