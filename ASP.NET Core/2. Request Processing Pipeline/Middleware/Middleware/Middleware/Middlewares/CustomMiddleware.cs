using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // Middleware logic: Writes a message to the response
            httpContext.Response.WriteAsync("Custom Middleware");

            // Call the next middleware in the pipeline
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomExtensions
    {
        public static IApplicationBuilder UseCustom(this IApplicationBuilder builder)
        {
            // UseMiddleware<T> adds the custom middleware to the pipeline
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
