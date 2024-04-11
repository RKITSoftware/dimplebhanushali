using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BasicAuth.Auth
{
    /// <summary>
    /// Custom authorization attribute for Basic Authentication.
    /// Denies access with Forbidden status code if user is not authenticated.
    /// </summary>
    public class BasicAuthorization : AuthorizeAttribute
    {
        /// <summary>
        /// Handles unauthorized requests by checking if the user is authenticated.
        /// If authenticated, calls base method to handle the request, otherwise returns Forbidden status code.
        /// </summary>
        /// <param name="actionContext">The context for the action being authorized.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}
