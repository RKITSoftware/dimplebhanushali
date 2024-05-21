using Middleware_API.Data;

namespace Middleware_API.Middlewares
{
    /// <summary>
    /// Middleware for performing authentication based on provided username and password.
    /// </summary>
    public class AuthenticationMiddleware
    {
        #region Private Member
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for AuthenticationMiddleware.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Public Async Method
        /// <summary>
        /// Invokes the authentication middleware.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            // Get username and password from request headers, query string, or request body
            var username = httpContext.Request.Query["username"].ToString();
            var password = httpContext.Request.Query["password"].ToString();

            // Check if the provided username and password match any user in the hardcoded data
            var user = UserData.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Authentication successful, set a flag in the HttpContext indicating authenticated user
                httpContext.Items["IsAuthenticated"] = true;
                await _next(httpContext);
            }
            else
            {
                // Authentication failed, return 401 Unauthorized response
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //await httpContext.Response.WriteAsync("Unauthorized: Invalid username or password");
            }
        }
        #endregion
    }

    #region Middleware Extension Class
    /// <summary>
    /// Extension method used to add the authentication middleware to the HTTP request pipeline.
    /// </summary>
    public static class AuthenticationMiddlewareExtensions
    {
        /// <summary>
        /// Adds the authentication middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
    #endregion
}
