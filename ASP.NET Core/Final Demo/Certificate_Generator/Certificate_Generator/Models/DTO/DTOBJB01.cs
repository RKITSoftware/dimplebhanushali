using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.DTO
{
    public class DTOBJB01
    {
        /// <summary>
        /// Job Id
        /// </summary>
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
    }
}
