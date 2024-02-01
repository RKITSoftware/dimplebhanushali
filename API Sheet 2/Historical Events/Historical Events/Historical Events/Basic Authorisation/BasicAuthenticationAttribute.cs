﻿using Historical_Events.Models;
using Historical_Events.User_Validation;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Historical_Events.Basic_Authorisation
{
    /// <summary>
    /// Custom Authorization Filter for handling Basic Authentication.
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Handles authorization by validating basic authentication credentials.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                // No authorization header present
                SetUnauthorizedResponse(actionContext, "Login Failed");
            }
            else
            {
                try
                {
                    // Decode and extract username and password from the authorization header
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    string[] userNamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(authToken)).Split(':');
                    string username = userNamePassword[0];
                    string password = userNamePassword[1];

                    if (ValidateUser.IsLogin(username, password))
                    {
                        // User authenticated successfully
                        usr01 userDetails = ValidateUser.GetUserDetails(username, password);

                        // Create a generic identity
                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name, userDetails.r01f03));

                        // Create a generic principal
                        IPrincipal principal = new GenericPrincipal(identity, userDetails.r01f05.ToString().Split(','));

                        // Set the current principal for the thread
                        Thread.CurrentPrincipal = principal;

                        // Set the user for the HttpContext if available
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            // HttpContext not available
                            throw new NotImplementedException("Authorization Denied");
                        }
                    }
                    else
                    {
                        // Invalid credentials
                        throw new NotImplementedException("Invalid Credentials");
                    }
                }
                catch (Exception)
                {
                    // General exception, internal server error
                    SetUnauthorizedResponse(actionContext, "Internal Server Error");
                }
            }
        }

        /// <summary>
        /// Sets an unauthorized response with the specified error message.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        /// <param name="message">The error message.</param>
        private void SetUnauthorizedResponse(HttpActionContext actionContext, string message)
        {
            actionContext.Response = actionContext.Request
                .CreateErrorResponse(System.Net.HttpStatusCode.Unauthorized, message);
        }
    }
}
