using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Web;

namespace Resume_Builder.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions and logging them.
    /// </summary>
    public class ExceptionMiddleware
    {
        #region Private Members
        /// <summary>
        /// Represents the next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger instance for logging exceptions.
        /// </summary>
        private readonly NLog.ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task representing the completion of request processing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext();
                var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>()) { Exception = ex };

                var filter = new Middlewares.Filters.ExceptionFilter();
                filter.OnException(exceptionContext);

                // Rethrow the exception after logging
                throw;
            }
        }
        #endregion
    }

    #region Extension Method for MIddleware
    /// <summary>
    /// Extension method to add the exception middleware to the HTTP request pipeline.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Adds the exception middleware to the application builder.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
    #endregion
}
