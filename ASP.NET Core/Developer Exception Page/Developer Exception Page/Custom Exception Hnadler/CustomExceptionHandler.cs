using Developer_Exception_Page.Error;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Developer_Exception_Page.Custom_Exception_Handler
{
    /// <summary>
    /// Handles exceptions that occur during request processing and generates appropriate error responses.
    /// </summary>
    public class CustomExceptionHandler
    {
        /// <summary>
        /// Handles the exception asynchronously and generates an error response.
        /// </summary>
        /// <param name="context">The HttpContext associated with the request.</param>
        public static async Task HandleExceptionAsync(HttpContext context)
        {
            // Retrieve the exception details from the IExceptionHandlerPathFeature
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            // Create an ErrorModel instance to encapsulate the error information
            var errorModel = new ErrorModel();

            // Set the RequestId to either the current activity ID or the trace identifier of the HTTP context
            errorModel.RequestId = Activity.Current?.Id ?? context.TraceIdentifier;

            // Set the ExceptionMsg based on the type of exception
            if (exception is FileNotFoundException)
            {
                errorModel.ExceptionMsg = "File not Found";
            }
            else if (exceptionHandlerPathFeature?.Path == "/")
            {
                errorModel.ExceptionMsg = "Page: Home";
            }
            else
            {
                // Default to the message of the caught exception
                errorModel.ExceptionMsg = exception.Message;
            }

            // Return the error response as JSON
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(errorModel);
        }
    }
}
