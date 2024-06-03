using Microsoft.AspNetCore.Mvc.Filters;
using MiddlewareWithFilters.Data;

namespace MiddlewareWithFilters.Middlewares
{
    /// <summary>
    /// Middleware for handling authorization.
    /// </summary>
    public class AuthorizationMiddleware
    {
        #region Private Member
        /// <summary>
        /// Represents the next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
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
            var dbFactory = context.RequestServices.GetRequiredService<DbConnectionFactory>();
            var filter = new Filters.AuthorizationFilter(dbFactory);

            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext(
                context,
                context.GetRouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
            );

            var authorizationFilterContext = new AuthorizationFilterContext(actionContext, new IFilterMetadata[] { });

            filter.OnAuthorization(authorizationFilterContext);

            if (authorizationFilterContext.Result != null)
            {
                context.Response.StatusCode = (authorizationFilterContext.Result as Microsoft.AspNetCore.Mvc.StatusCodeResult)?.StatusCode ?? StatusCodes.Status403Forbidden;
                return;
            }

            await _next(context);
        }
        #endregion
    }

    #region Extension Method for Middleware

    /// <summary>
    /// Extension method to add the authorization middleware to the HTTP request pipeline.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the authorization middleware to the application builder.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
    #endregion
}
