using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace MiddlewareWithFilters.Models.POCO
{
    /// <summary>
    /// Model Representing User Class.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [PrimaryKey,AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string R01F04 { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        public string R01F05 { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string R01F06 { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        [IgnoreOnUpdate]
        [DefaultValue(null)]
        public DateTime R01F07 { get; set; }

        /// <summary>
        /// UpdatedAt
        /// </summary>
        [IgnoreOnInsert]
        [DefaultValue(null)]
        public DateTime? R01F08 { get; set; }
    }
}
