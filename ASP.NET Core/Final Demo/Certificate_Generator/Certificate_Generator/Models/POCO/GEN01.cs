using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// Generated Certificate Model
    /// </summary>
    public class GEN01
    {
        /// <summary>
        /// Certificate Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int N01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int N01F02 { get; set; }

        /// <summary>
        /// Template Id
        /// </summary>
        public int N01F03 { get; set; }
        
        /// <summary>
        /// Generation Date
        /// </summary>
        public DateTime N01F04 { get; set; }
        
        /// <summary>
        /// File Path
        /// </summary>
        public string N01F05 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime N01F06 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime N01F07 { get; set; }

    }
}
