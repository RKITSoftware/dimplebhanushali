using Historical_Events.BL;
using Historical_Events.Models;
using System.Linq;

namespace Historical_Events.User_Validation
{
    public class ValidateUser
    {
        /// <summary>
        /// Validates user login credentials.
        /// </summary>
        /// <param name="username">The username to be validated.</param>
        /// <param name="encryptedPassword">The encrypted password to be validated.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public static bool IsLogin(string username, string password)
        {
            string encryptedPassword = BLAES.Encrypt(password);
            // Check if there is any user with the provided username and encrypted password
            return BLUser.GetAllUsers()
                         .Any(user => user.r01f03.Equals(username) && user.r01f04 == encryptedPassword);
        }

        /// <summary>
        /// Retrieves user details based on provided login credentials.
        /// </summary>
        /// <param name="username">The username for which details are requested.</param>
        /// <param name="encryptedPassword">The encrypted password for which details are requested.</param>
        /// <returns>The User object if found, otherwise null.</returns>
        public static usr01 GetUserDetails(string username, string encryptedPassword)
        {
            // Retrieve the user details based on the provided username and encrypted password
            return BLUser.GetAllUsers().FirstOrDefault(user => user.r01f03.Equals(username) && user.r01f04 == encryptedPassword);
        }
    }
}
