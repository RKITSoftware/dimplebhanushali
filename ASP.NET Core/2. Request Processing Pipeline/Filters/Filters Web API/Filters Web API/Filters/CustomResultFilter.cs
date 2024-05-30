using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom result filter to log information before and after a result is executed.
    /// </summary>
    public class CustomResultFilter : IResultFilter
    {
        #region Private Member
        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomResultFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log information.</param>
        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method called before a result is executed.
        /// </summary>
        /// <param name="context">The context of the result executing.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation($"Result executing for: {context.ActionDescriptor.DisplayName}");
        }

        /// <summary>
        /// Method called after a result is executed.
        /// </summary>
        /// <param name="context">The context of the result executed.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation($"Result executed for: {context.ActionDescriptor.DisplayName}");
        }
        #endregion
    }
}
