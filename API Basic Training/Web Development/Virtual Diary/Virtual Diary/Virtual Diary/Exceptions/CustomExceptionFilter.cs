using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Virtual_Diary.Exceptions
{
    /// <summary>
    /// Represents a custom exception filter in the Virtual Diary application.
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
            string errorMessage = ShowError(actionExecutedContext.Exception);

            // Create a response message with InternalServerError status, content, and reason phrase.
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Oops! Something went wrong: {errorMessage}. We are working to fix the issue. Please try again later."),
                ReasonPhrase = "Internal Server Error"
            };

            // Set the response to be sent back to the client.
            actionExecutedContext.Response = response;
        }

        /// <summary>
        /// Logs or handles the exception and returns a message indicating the handled exception.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A message indicating the handled exception.</returns>
        public string ShowError(Exception exception)
        {
            try
            {
                // Log or handle the exception as needed.
                return exception.Message;
            }
            catch (Exception ex)
            {
                // Handle any further exceptions that might occur during logging or handling.
                return $"Error occurred while handling exception: {ex.Message}";
            }
        }
    }
}
