using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuth.Auth
{
    /// <summary>
    /// Authorization filter for basic authentication.
    /// </summary>
    public class BasicAuthentication : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Gets or sets the allowed roles for the decorated method or controller.
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Overrides the default authorization to perform basic authentication.
        /// </summary>
        /// <param name="actionContext">The context for the HTTP action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
            }
            else
            {
                try
                {
                  
                    //username:password  base64 encoded
                    //admin:password

                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                    string[] usernamePassword = decodedToken.Split(':');

                    string username = usernamePassword[0];
                    string password = usernamePassword[1];

                    if (ValidateUSer.Login(username, password))
                    {
                        string[] userRoles = ValidateUSer.GetUserRoles(username);
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), userRoles);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                       .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
                    }
                }
                catch (Exception)
                {
                    actionContext.Response = actionContext.Request
                       .CreateErrorResponse(HttpStatusCode.InternalServerError, "Server error - Login Failed");
                }

            }
        }
    }
}
