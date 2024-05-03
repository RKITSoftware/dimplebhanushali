using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.DTO
{
    public class DTODAT01
    {
        /// <summary>
        /// Data Id
        /// </summary>
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
    }
}
