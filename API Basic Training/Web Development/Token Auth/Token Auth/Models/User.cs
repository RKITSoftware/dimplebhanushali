using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Token_Auth.Models
{
    /// <summary>
    /// Represents a user with authentication and authorization details.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username for authentication.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the roles assigned to the user.
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// List of sample users with authentication and authorization details.
        /// </summary>
        public static List<User> lstUsers = new List<User>
        {
            new User { UserId = 1, UserName="dimple", Password="12345", Roles="admin", Email="abc@gmail.com"},
            new User { UserId = 2, UserName="ankit", Password="12345", Roles="superadmin", Email="xyz@gmail.com"},
            new User { UserId = 3, UserName="shiva", Password="12345", Roles="user", Email="shiva@gmail.com"},
        };
    }
}
