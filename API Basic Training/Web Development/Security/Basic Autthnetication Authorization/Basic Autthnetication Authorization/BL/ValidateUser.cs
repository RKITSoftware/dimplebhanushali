using Basic_Autthnetication_Authorization.Models;
using System.Linq;

namespace Basic_Autthnetication_Authorization.BL
{
    /// <summary>
    /// Provides methods for validating user credentials and retrieving user details.
    /// </summary>
    public class ValidateUser
    {
        #region Authentication

        /// <summary>
        /// Checks if a user with the specified credentials exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the user exists; otherwise, false.</returns>
        public static bool Exist(string username, string password)
        {
            // Check if any user matches the given username and password.
            return BLUser.UsersDeatil().Any(user => user.UserName.Equals(username) && user.Password == password);
        }

        #endregion

        #region User Retrieval

        /// <summary>
        /// Retrieves the details of a user with the specified credentials.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user details if found; otherwise, null.</returns>
        public static User CollectUserDetail(string username, string password)
        {
            // Retrieve the first user that matches the given username and password.
            return BLUser.UsersDeatil().FirstOrDefault(user => user.UserName.Equals(username) && user.Password == password);

        }
        #endregion
    }
}