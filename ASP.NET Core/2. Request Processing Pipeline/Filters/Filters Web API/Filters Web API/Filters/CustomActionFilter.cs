using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom action filter to log information before and after an action executes.
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        #region Private Member
        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomActionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log information.</param>
        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method called before an action executes.
        /// </summary>
        /// <param name="context">The context of the action executing.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action executing: {context.ActionDescriptor.DisplayName}");
        }

        /// <summary>
        /// Method called after an action executes.
        /// </summary>
        /// <param name="context">The context of the action executed.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action executed: {context.ActionDescriptor.DisplayName}");
        }
        #endregion
    }
}
