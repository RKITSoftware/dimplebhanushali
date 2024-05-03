using System.Security.Claims;

namespace Resume_Builder.Helpers
{
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


        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> email id </returns>
        public static string GetEmailIdFromClaims(this HttpContext httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;
            var userIdClaim = principal.FindFirst(c => c.Type == "jwt_eamilId");

            return userIdClaim.Value;
        }
    }
}
