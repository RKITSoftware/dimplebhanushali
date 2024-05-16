using Basic_Autthnetication_Authorization.Models;
using System.Collections.Generic;

namespace Basic_Autthnetication_Authorization.BL
{
    /// <summary>
    /// Business logic class for managing users.
    /// </summary>
    public class BLUser
    {
        /// <summary>
        /// Retrieve a list of user details.
        /// </summary>
        /// <returns>List of User objects.</returns>
        public static List<User> UsersDeatil()
        {
            // Create a list of User objects with sample data.
            List<User> objUsers = new List<User>()
            {
                new User{ Id = 101, UserName = "admin", Password = "admin123", Email ="admin@gmail.com", Role = "Admin"},
                new User{ Id = 102, UserName = "superAdmin", Password = "superadmin123", Email ="superadmin@gmail.com", Role = "SuperAdmin"},
                new User{ Id = 103, UserName = "user", Password = "user123", Email ="user@gmail.com", Role = "User"},
            };
            // Return the list of users.
            return objUsers;
        }
    }
}
