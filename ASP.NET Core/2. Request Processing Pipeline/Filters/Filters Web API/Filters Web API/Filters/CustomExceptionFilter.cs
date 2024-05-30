using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom exception filter to log unhandled exceptions.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        #region Private Member
        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log exception information.</param>
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Method called when an unhandled exception occurs.
        /// </summary>
        /// <param name="context">The exception filter context.</param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"Exception occurred: {context.Exception.Message}");
        }
        #endregion
    }
}
