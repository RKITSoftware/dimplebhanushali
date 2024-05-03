using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.DTO
{
    public class DTOGEN01
    {
        /// <summary>
        /// Certificate Id
        /// </summary>
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
    }
}
