using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MiddlewareWithFilters.Data;
using MiddlewareWithFilters.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace MiddlewareWithFilters.Middlewares.Filters
{
    /// <summary>
    /// Authorization Filter.
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        #region Private Member
        /// <summary>
        /// Db Connection Factory Instance.
        /// </summary>
        private readonly DbConnectionFactory _db;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Initialising Db Factory Instance.
        /// </summary>
        /// <param name="dbContext">DbConnectionFactory</param>
        public AuthorizationFilter(DbConnectionFactory dbContext)
        {
            _db = dbContext;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="context">Context of authorization filter</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the endpoint information
            var endpoint = context.HttpContext.GetEndpoint();

            // Check if the endpoint is excluded from authentication
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return; // Skip authentication for this endpoint
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
                            identity.AddClaim(new Claim(ClaimTypes.Name, user.R01F04));
                            identity.AddClaim(new Claim("Id", Convert.ToString(user.R01F01)));

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
                    catch (Exception ex)
                    {
                        // Log exception
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Result = new JsonResult(new { Message = "An error occurred while processing your request." });
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new JsonResult(new { Message = "An error occurred while processing your request." });
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new JsonResult(new { Message = "An error occurred while processing your request." });
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
            var user = UserDetails(username, password);
            return user != null;
        }

        /// <summary>
        /// Returns user with current credential
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>Object of class USR01</returns>
        private USR01 UserDetails(string username, string password)
        {
            using (IDbConnection db = _db.CreateConnection())
            {
                var user = db.Single<USR01>(u => u.R01F04 == username && u.R01F05 == password);
                return user;
            }
        }
        #endregion
    }
}
