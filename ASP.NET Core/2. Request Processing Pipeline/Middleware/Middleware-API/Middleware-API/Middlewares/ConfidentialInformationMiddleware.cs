using Middleware_API.Models;

namespace Middleware_API.Middlewares
{
    /// <summary>
    /// Middleware for returning confidential information such as email and phone number of the user.
    /// </summary>
    public class ConfidentialInformationMiddleware
    {
        #region Private Member
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for ConfidentialInformationMiddleware.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        public ConfidentialInformationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Public Async Methods
        /// <summary>
        /// Invokes the confidential information middleware.
        /// </summary>f
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            // Check if user data exists in the HttpContext.Items
            if (httpContext.Items.ContainsKey("UserData"))
            {
                // Retrieve user data from HttpContext.Items
                var user = (User)httpContext.Items["UserData"];

                // Return email and phone number of the user
                await httpContext.Response.WriteAsync($"Email: {user.Email}, Phone: {user.Phone}");
                await _next(httpContext);
            }
            else
            {
                // User data not found, return 404 Not Found response
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync("User data not found");
            }
        }
        #endregion
    }

    #region MIddleware Extension
    /// <summary>
    /// Extension method used to add the confidential information middleware to the HTTP request pipeline.
    /// </summary>
    public static class ConfidentialInformationMiddlewareExtensions
    {
        /// <summary>
        /// Adds the confidential information middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseConfidentialInformationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConfidentialInformationMiddleware>();
        }
    }
    #endregion
}
