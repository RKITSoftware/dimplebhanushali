using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing a User.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// First Name of User
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Last Name of User
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Email Id of User
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Mobile Number of User
        /// </summary>
        public string R01F05 { get; set; }

        /// <summary>
        /// Age of User
        /// </summary>
        public int R01F06{ get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F07 { get; set; }

        /// <summary>
        /// User Created Date
        /// </summary>
        public DateTime? R01F08 { get; set; }

        /// <summary>
        /// User Updated Date
        /// </summary>
        public DateTime? R01F09 { get; set; }
    }
}
