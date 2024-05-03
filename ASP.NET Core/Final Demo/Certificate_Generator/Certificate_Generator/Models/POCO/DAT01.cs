using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// Certificate Data
    /// </summary>
    public class DAT01
    {
        /// <summary>
        /// Data Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int T01F01 { get; set; }

        /// <summary>
        /// Certificate Id
        /// </summary>
        public int T01F02 { get; set; }

        /// <summary>
        /// Field Name
        /// </summary>
        public string T01F03 { get; set; }
        
        /// <summary>
        /// Field Value
        /// </summary>
        public string T01F04 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime T01F05 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime T01F06 { get; set; }

    }
}
