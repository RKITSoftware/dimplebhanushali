using Historical_Events.User_Validation;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Historical_Events.Basic_Authorisation
{
    /// <summary>
    /// Custom Authorization Filter for handling Basic Authentication.
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods

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
                    
                    string token = authToken.Substring("Bearer ".Length).Trim();

                    BLValidateUser blValidateUser = new BLValidateUser();
                    
                    if(blValidateUser.ValidateJwtToken(token))
                    {
                        return;
                    }
                    else
                    {
                        SetUnauthorizedResponse(actionContext, "Login Failed");
                    }

                }
                catch (Exception)
                {
                    // General exception, internal server error
                    SetUnauthorizedResponse(actionContext, "Internal Server Error");
                }
            }
        }

        #endregion

        #region Private Methods

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

        #endregion
    }
}
