using System.Security.Claims;

namespace Resume_Builder.Helpers
{
    /// <summary>
    /// Utility methods for common operations.
    /// </summary>
    public static class Utility
    {
        #region Public Methods

        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> user id </returns>
        public static int GetUserIdFromClaims(this HttpContext httpContext)
        {
            int userId = 0;
            try
            {
                var principal = httpContext.User as ClaimsPrincipal;
                var userIdClaim = principal.FindFirst(c => c.Type == "jwt_userId");
                userId = Convert.ToInt32(userIdClaim.Value);
            }
            catch (Exception)
            {
            }

            return userId;
        }

        /// <summary>
        /// Extension method to get user id from jwt claims
        /// </summary>
        /// <param name="httpContext"> http context </param>
        /// <returns> email id </returns>
        public static string GetEmailIdFromClaims(this HttpContext httpContext)
        {
            string email = string.Empty;
            try
            {
                var principal = httpContext.User as ClaimsPrincipal;
                var userIdClaim = principal.FindFirst(c => c.Type == "jwt_eamilId");
                if (userIdClaim != null)
                {
                    email = userIdClaim.Value;
                }
            }
            catch (Exception)
            {
            }
            return email;
        }

        #endregion
    }
}
