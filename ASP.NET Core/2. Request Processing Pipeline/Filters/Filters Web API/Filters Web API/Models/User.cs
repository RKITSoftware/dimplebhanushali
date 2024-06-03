namespace Filters_Web_API.Models
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public enmUserRole R01F04 { get; set; } = enmUserRole.U;
    }

    public enum enmUserRole
    {
        A,
        U
    }
}
