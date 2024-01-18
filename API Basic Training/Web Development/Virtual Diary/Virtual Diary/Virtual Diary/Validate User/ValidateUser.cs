using System.Linq;
using Virtual_Diary.Models;

namespace Virtual_Diary
{
    /// <summary>
    /// Provides methods to validate user credentials and retrieve user details.
    /// </summary>
    internal class ValidateUser
    {
        /// <summary>
        /// Validates user login credentials.
        /// </summary>
        /// <param name="username">The username to be validated.</param>
        /// <param name="password">The password to be validated.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public static bool IsLogin(string username, string password)
        {
            // Check if there is any user with the provided username and password
            return User.GetUsers().Any(user => user.UserName.Equals(username) && user.Password == password);
        }

        /// <summary>
        /// Retrieves user details based on provided login credentials.
        /// </summary>
        /// <param name="username">The username for which details are requested.</param>
        /// <param name="password">The password for which details are requested.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        public static User GetUserDetails(string username, string password)
        {
            // Retrieve the user details based on the provided username and password
            return User.GetUsers().FirstOrDefault(user => user.UserName.Equals(username) && user.Password == password);
        }
    }
}
