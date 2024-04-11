using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Basic_Authentication.Authorisation_Provider
{
    /// <summary>
    /// Provides authorization based on basic authentication credentials.
    /// </summary>
    public class Authorisation : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Gets or sets the roles required for authorization. Multiple roles can be specified using commas.
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets an array of allowed roles for authorization.
        /// </summary>
        public string[] AllowRoles { get; set; }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="actionContext">The context for the action being authorized.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                // No Authorization header provided, return unauthorized response
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
            }
            else
            {
                try
                {
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                    string[] usernamePassword = decodedToken.Split(':');

                    if (usernamePassword.Length == 2)
                    {
                        string username = usernamePassword[0];
                        string password = usernamePassword[1];


                        if (ValidateUser.IsLogin(username, password))
                        {
                            // Valid user, set the user roles
                            string[] userRoles = ValidateUser.GetUserRoles(username);
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), userRoles);
                        }
                        else
                        {
                            // Invalid credentials, return unauthorized response
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Login Failed");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle unexpected errors and return internal server error
                    actionContext.Response = actionContext.Request
                       .CreateErrorResponse(HttpStatusCode.InternalServerError, "Server error - Login Failed");
                }
            }
        }
    }
}
