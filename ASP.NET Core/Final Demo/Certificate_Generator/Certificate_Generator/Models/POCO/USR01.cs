using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// USer Model
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public string R01F05 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F06 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime R01F07 { get; set; }
    }
}
