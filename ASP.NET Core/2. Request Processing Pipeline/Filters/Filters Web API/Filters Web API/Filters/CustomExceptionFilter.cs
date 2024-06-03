using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Web;
using System.Diagnostics;
using System.Reflection;

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
        private readonly NLog.ILogger _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger to log exception information.</param>
        public CustomExceptionFilter()
        {
            _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger(); 
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Logs exception into file 
        /// </summary>
        /// <param name="context">Context of exception filter</param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            ////
            StackTrace objStack = new StackTrace(exception);
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            string methodname = objStack.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisAssembly).Name;

            string message = string.Format($"{exception.GetType()} | {methodname} | {exception.Message} \n{exception.StackTrace}\n");

            _logger.Error(message);

            throw exception;
        }
        #endregion
    }
}
