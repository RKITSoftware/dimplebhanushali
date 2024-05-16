using System;
using System.Linq;
using Token_Auth.Models;

namespace Token_Auth
{
    /// <summary>
    /// Repository class for user-related operations and user validation.
    /// </summary>
    public class UserRepo : IDisposable
    {
        /// <summary>
        /// Validates a user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user to validate.</param>
        /// <param name="password">The password of the user to validate.</param>
        /// <returns>The user object if the validation is successful; otherwise, null.</returns>
        public User ValidateUser(string username, string password)
        {
            // Using LINQ to find a user with matching username and password
            return User.lstUsers.FirstOrDefault(
                user => user.UserName.Equals(username)
                   && user.Password == password);
        }

        /// <summary>
        /// Disposes of resources if needed.
        /// </summary>
        public void Dispose()
        {
            // Implement IDisposable pattern if needed
        }
    }
}
