using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Virtual_Diary.Exceptions
{
    /// <summary>
    /// Represents a custom exception in the Virtual Diary application.
    /// </summary>
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Overrides the OnException method to handle exceptions during the processing of an HTTP request.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action that threw the exception.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // Log or handle the exception and generate a response.
            ShowError(actionExecutedContext.Exception);

            // Create a response message with InternalServerError status, content, and reason phrase.
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Some Error Occurred !!!"),
                ReasonPhrase = "Internal Server Error !!!"
            };

            // Set the response to be sent back to the client.
            actionExecutedContext.Response = response;
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
                return "Exception Handled";
            }
            catch (Exception ex)
            {
                // Handle any further exceptions that might occur during logging or handling.
                return $"Error Occurred => {ex.Message}";
            }
        }
    }
}
