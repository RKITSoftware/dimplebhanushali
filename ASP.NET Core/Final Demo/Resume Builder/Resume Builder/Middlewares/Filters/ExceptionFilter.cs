using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Web;
using System.Diagnostics;
using System.Reflection;

namespace Resume_Builder.Middlewares.Filters
{
    /// <summary>
    /// Custom exception filter to log unhandled exceptions.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        #region Private Member
        /// <summary>
        /// ILogger
        /// </summary>
        private readonly NLog.ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log exception information.</param>
        public ExceptionFilter()
        {
            _logger = NLogBuilder.ConfigureNLog("Nlog.config").GetCurrentClassLogger();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Logs exception into file 
        /// </summary>
        /// <param name="context">Context of exception filter</param>
        public void OnException(ExceptionContext context)
        {
            // Retrieve the exception causing the error from the provided ExceptionContext
            Exception exception = context.Exception;

            // Create a StackTrace object to capture information about
            // the call stack at the point where the exception occurred
            StackTrace objStack = new StackTrace(exception);

            // Get the currently executing assembly
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            // Extract the name of the method where the exception occurred from the call stack
            string methodname = objStack
                .GetFrames() // Get all stack frames
                .Select(f => f.GetMethod()) // Get method information for each frame
                .First(m => m.Module.Assembly == thisAssembly) // Filter out the method from the current assembly
                .Name; // Get the name of the method

            // Format the message to be logged, including exception type, method name, message, and stack trace
            string message = string.Format($"{exception.GetType()} | {methodname} | {exception.Message} \n{exception.StackTrace}\n");

            // Log the formatted message with the logger
            _logger.Error(message);

            // Rethrow the exception to allow further handling by other exception filters or middleware
            throw exception;
        }

        #endregion
    }
}
