using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Basic_Autthnetication_Authorization.Authorization_Provider
{
    /// <summary>
    /// Custom authorization attribute for basic authentication.
    /// </summary>
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Handles unauthorized requests based on authentication status.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Checks if the user is authenticated.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // Proceeds with the base unauthorized request handling.
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // Sets the response to forbidden for unauthenticated users.
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}