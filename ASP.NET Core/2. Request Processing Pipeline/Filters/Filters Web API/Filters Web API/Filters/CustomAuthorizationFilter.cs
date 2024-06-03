using Filters_Web_API.BL;
using Filters_Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Filters_Web_API.Filters
{
    /// <summary>
    /// Custom authorization filter to handle basic authentication.
    /// </summary>
    public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        #region Public Method
        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any();
            if (allowAnonymous)
            {
                return;
            }

            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    try
                    {
                        string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderVal.Parameter));

                        string[] userInfo = credentials.Split(':');
                        string username = userInfo[0];
                        string password = userInfo[1];

                        if (ValidateUser(username, password))
                        {
                            var user = UserDetails(username, password);

                            var identity = new GenericIdentity(username);
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                            identity.AddClaim(new Claim("Id", Convert.ToString(user.UserId)));


                            IPrincipal principal = new GenericPrincipal(identity, user.R01F04.ToString().Split(','));

                            Thread.CurrentPrincipal = principal;

                            context.HttpContext.User = (ClaimsPrincipal)principal;
                            return;
                        }
                        else
                        {
                            context.Result = new UnauthorizedResult();
                        }
                    }
                    catch (FormatException)
                    {
                        // Credentials were not formatted correctly.
                        context.HttpContext.Response.StatusCode = 401;
                    }
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True if user is valid, false otherwise</returns>
        private bool ValidateUser(string username, string password)
        {
            var user = BLUser.lstUser.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns user with current credential
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Object of class USR01</returns>
        private User UserDetails(string username, string password)
        {
            var user = BLUser.lstUser.FirstOrDefault(u => u.UserName == username && u.Password == password);

            return user;
        }
        #endregion
    }
}
