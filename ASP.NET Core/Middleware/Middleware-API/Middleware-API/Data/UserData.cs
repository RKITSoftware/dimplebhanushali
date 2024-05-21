using Middleware_API.Models;

namespace Middleware_API.Data
{
    /// <summary>
    /// Represents a data repository for user information.
    /// </summary>
    public class UserData
    {
        #region User Data
        /// <summary>
        /// Gets the list of hardcoded user data.
        /// </summary>
        public static List<User> Users { get; } = new List<User>
        {
            // Hardcoded user data
            new User
            {
                Username = "dimple",
                Password = "dimple",
                Email = "dimple@gmail.com",
                Phone = 9624863508,
                City = "New York"
            },
            new User
            {
                Username = "ankit",
                Password = "akbhanu",
                Email = "ankit@gmail.com",
                Phone = 9876543210,
                City = "Los Angeles"
            },
            new User
            {
                Username = "pankaj",
                Password = "pankaj",
                Email = "pankaj@gmail.com",
                Phone = 9966887755,
                City = "Chicago"
            },
        };

        #endregion
    }
}
