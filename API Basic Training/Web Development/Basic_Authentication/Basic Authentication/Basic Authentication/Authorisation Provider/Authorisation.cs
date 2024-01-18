using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Security.Principal;

namespace Basic_Authentication.Authorisation_Provider
{
    public class Authorisation : AuthorizationFilterAttribute
    {
        public string Roles { get; set; }
        public string[] AllowRoles { get; set; }

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
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;

                    string[] usernamePassword = authToken.Split(':');

                    string username = usernamePassword[0];
                    string password = usernamePassword[1];

                    if (ValidateUser.IsLogin(username, password))
                    {
                        string[] userRoles = ValidateUser.GetUserRoles(username);
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), userRoles);

                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Login Failed");

                    }

                }
                catch (Exception ex)
                {
                    actionContext.Response = actionContext.Request
                       .CreateErrorResponse(HttpStatusCode.InternalServerError, "Server error - Login Failed");

                }
            }
        }

    }
}