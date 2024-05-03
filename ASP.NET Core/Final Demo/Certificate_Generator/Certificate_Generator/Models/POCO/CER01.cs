using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// CertificateTemplate Model
    /// </summary>
    public class CER01
    {
        /// <summary>
        /// Template Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
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

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F05 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime R01F06 { get; set; }

    }
}
