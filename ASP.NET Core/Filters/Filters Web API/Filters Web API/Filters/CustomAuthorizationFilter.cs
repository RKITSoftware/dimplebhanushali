using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Filters_Web_API.Filters
{
    public class CustomAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger _logger;

        public CustomAuthorizationFilter(ILogger<CustomAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Retrieve the authorization header
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // Get the value of the Authorization header
            var authHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            // Check if it's in the correct format: "Basic base64encoded(username:password)"
            if (!authHeader.StartsWith("Basic "))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // Extract credentials from the authorization header
            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // Check if the credentials are valid
            if (!(username == "dimple" && password == "dimple"))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // Credentials are valid
            _logger.LogInformation($"User '{username}' is authorized.");
        }
    }
}
