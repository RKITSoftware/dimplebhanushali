using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Exception_Demo
{
    /// <summary>
    /// Exception filter for handling exceptions in the CustomerController of a Web API application.
    /// </summary>
    public class CustomerExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Overrides the OnException method to handle exceptions during the processing of an HTTP request.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action that threw the exception.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            //string errorMsg = "";
            string errorMsg = ShowError(actionExecutedContext.Exception);

            // get the exception type
            Type exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                httpStatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
            }

            // generate a response message
            HttpResponseMessage response = new HttpResponseMessage(httpStatusCode)
            {
                Content = new StringContent(errorMsg)
            };

            actionExecutedContext.Response = response;

            // Log or handle the exception
            // Depending on your logging mechanism, you might want to log the error message

            base.OnException(actionExecutedContext);
        }

        /// <summary>
        /// Logs or handles the exception and returns a message indicating that the exception has been handled.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A message indicating that the exception has been handled.</returns>
        public string ShowError(Exception exception)
        {
            try
            {
                // Log or handle the exception as needed.
                return exception.Message.ToString();
            }
            catch (Exception ex)
            {
                // Handle any further exceptions that might occur during logging or handling.
                return $"Error Occurred => {ex.Message}";
            }
        }
    }
}
