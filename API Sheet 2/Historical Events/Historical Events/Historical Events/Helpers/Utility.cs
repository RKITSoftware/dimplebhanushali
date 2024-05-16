using System;
using System.Security.Claims;
using System.Web;

namespace Historical_Events.Helpers
{
    /// <summary>
    /// Class For Handling Other Utilities.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> user id </returns>
        public static int GetUserIdFromClaims(this HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userIdClaim = principal.FindFirst(c => c.Type == "jwt_userId");

            return Convert.ToInt32(userIdClaim.Value);
        }
    }
}