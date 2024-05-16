
namespace BasicAuth
{
    /// <summary>
    /// Class for user authentication.
    /// </summary>
    public class ValidateUSer
    {
        /// <summary>
        /// Validates user login credentials.
        /// </summary>
        /// <param name="username">The username to be validated.</param>
        /// <param name="password">The password to be validated.</param>
        /// <returns>True if the username and password are valid, otherwise false.</returns>
        public static bool Login(string username, string password)
        {
            return (username == "admin" && password == "password") ||
                  (username == "employee" && password == "password");
        }
        public static string[] GetUserRoles(string username)
        {
            // Replace this with your actual role-checking logic
            // For demo purposes, assigning roles based on username
            if (username == "admin")
            {
                return new[] { "Admin" };
            }
            else if (username == "employee")
            {
                return new[] { "Employee" };
            }
            else
            {
                return null;
            }
        }

    }
}
