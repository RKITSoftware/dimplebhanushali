using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http;

namespace Historical_Events_API.BasicAuth
{
    /// <summary>
    /// Custom authorization attribute for handling basic authentication in Web API.
    /// </summary>
    public class BasicAuthorisationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Handles unauthorized requests by either calling the base implementation or setting the response to Forbidden.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Check if the user is authenticated
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // If authenticated, call the base implementation
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // If not authenticated, set the response to Forbidden (HTTP 403)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}