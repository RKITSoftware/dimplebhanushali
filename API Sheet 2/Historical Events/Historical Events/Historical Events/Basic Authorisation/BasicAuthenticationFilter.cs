using Historical_Events.User_Validation;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Historical_Events.Basic_Authorisation
{
    /// <summary>
    /// Custom Authorization Filter for handling Basic Authentication.
    /// </summary>
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Handles authorization by validating basic authentication credentials.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;
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

                    if (blValidateUser.ValidateJwtToken(authToken))
                    {
                        return;
                    }
                    else
                    {
                        SetUnauthorizedResponse(actionContext, "Login Failed");
                    }

                }
                catch (Exception ex)
                {
                    // General exception, internal server error
                    SetUnauthorizedResponse(actionContext, ex.Message);
                }
            }
        }

        /// <summary>
        /// To allow anonymous users to access endpoint
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool SkipAuthorization(HttpActionContext actionContext)
        {
            // Use Contract.Assert to ensure that the actionContext is not null.
            Contract.Assert(actionContext != null);

            // Check if the action or controller has the AllowAnonymousAttribute.
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
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
