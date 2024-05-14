using System.Collections.Generic;

namespace Virtual_Diary.Models
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or Sets User Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets User Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets User User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Roles
        /// </summary>
        public string Roles { get; set; }

       
    }
}