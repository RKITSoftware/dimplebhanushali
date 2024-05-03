using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.DTO
{
    public class DTOCER01
    {
        /// <summary>
        /// Template Id
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Template Name
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Template Type
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Template Content
        /// </summary>
        public string R01F04 { get; set; }
    }
}
