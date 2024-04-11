using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Filters.Authorisation_FIlter
{
    public class MyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ILogger<MyAuthorizationFilter> _logger;

        public MyAuthorizationFilter(ILogger<MyAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Perform authorization logic here
            if (!IsUserAuthorized(context))
            {
                _logger.LogInformation("Unauthorized access attempted.");
                context.Result = new UnauthorizedResult(); // Return 401 Unauthorized response
            }
        }

        private bool IsUserAuthorized(AuthorizationFilterContext context)
        {
            // Dummy authorization logic, replace with your actual authorization logic
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
