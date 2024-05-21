using Middleware_API.Data;

namespace Middleware_API.Middlewares
{
    /// <summary>
    /// Middleware for retrieving user data based on authentication status.
    /// </summary>
    public class DataRetrievalMiddleware
    {
        #region Private Member
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for DataRetrievalMiddleware.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        public DataRetrievalMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Public Async Method
        /// <summary>
        /// Invokes the data retrieval middleware.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            // Check if the user is authenticated
            if (httpContext.Items.ContainsKey("IsAuthenticated") && (bool)httpContext.Items["IsAuthenticated"])
            {
                // Retrieve username and password from the query string
                var username = httpContext.Request.Query["username"].ToString();
                var password = httpContext.Request.Query["password"].ToString();

                // Retrieve user data based on the provided username and password
                var user = UserData.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // Add user data to the HttpContext.Items to pass to the next middleware
                    httpContext.Items["UserData"] = user;

                    // Call the next middleware
                    await _next(httpContext);
                }
                else
                {
                    // User data not found, return 404 Not Found response
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    await httpContext.Response.WriteAsync("Data not found");
                }
            }
            else
            {
                // User is not authenticated, return 401 Unauthorized response
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized: You must be authenticated to retrieve data.");
            }
        }
        #endregion
    }

    #region Middleware Extension Class
    /// <summary>
    /// Extension method used to add the data retrieval middleware to the HTTP request pipeline.
    /// </summary>
    public static class DataRetrievalMiddlewareExtensions
    {
        /// <summary>
        /// Adds the data retrieval middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseDataRetrievalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DataRetrievalMiddleware>();
        }
    }
    #endregion
}
