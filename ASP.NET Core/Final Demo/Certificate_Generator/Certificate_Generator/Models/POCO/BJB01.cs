using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// Bulk Certificate Job
    /// </summary>
    public class BJB01
    {
        /// <summary>
        /// Job Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int B01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int B01F02 { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string B01F03 { get; set; }

        /// <summary>
        /// File Path
        /// </summary>
        public string B01F04 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime B01F05 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime B01F06 { get; set; }

    }
}
