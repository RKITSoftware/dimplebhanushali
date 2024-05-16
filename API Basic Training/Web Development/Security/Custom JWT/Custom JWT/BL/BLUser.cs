using Custom_JWT.Models;
using System.Collections.Generic;
using System.Linq;

namespace Custom_JWT.BL
{
    /// <summary>
    /// BLUser Representing User's Methods
    /// </summary>
    public class BLUser
    {
        // Sample list of users for demonstration purposes
        private static List<User> _lstUsers = new List<User>
        {
            new User {Id = 1, Name = "Dimple", Password = "dimple123", Role = "Admin"},
            new User {Id = 2, Name = "Ankit", Password = "ankit123", Role = "User"}
        };

        /// <summary>
        /// Gets a user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public static User GetUser(string username, string password)
        {
            // Using LINQ to find the user based on the provided username and password
            return _lstUsers.FirstOrDefault(user =>
                        user.Name.Equals(username) && user.Password.Equals(password));
        }

        /// <summary>
        /// Gets a user based on the provided user ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        internal static User GetUser(int id)
        {
            // Using LINQ to find the user based on the provided user ID
            return _lstUsers.FirstOrDefault(user => user.Id == id);
        }
    }
}