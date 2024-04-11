using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"Exception occurred: {context.Exception.Message}");
        }
    }
}
