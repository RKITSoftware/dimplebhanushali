namespace Basic_Authentication
{
    /// <summary>
    /// Utility class for user authentication and role assignment.
    /// </summary>
    public class ValidateUser
    {
        /// <summary>
        /// Validates user login credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public static bool IsLogin(string username, string password)
        {
            return (username == "admin" && password == "password") ||
                   (username == "employee" && password == "password");
        }

        /// <summary>
        /// Gets the roles associated with a user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>An array of roles assigned to the user or null if the user is not found.</returns>
        public static string[] GetUserRoles(string username)
        {
            // For demo purposes, assigning roles based on username
            switch (username)
            {
                case "admin":
                    return new[] { "Admin" };
                case "employee":
                    return new[] { "Employee" };
                default:
                    return null;
            }
        }
    }
}
