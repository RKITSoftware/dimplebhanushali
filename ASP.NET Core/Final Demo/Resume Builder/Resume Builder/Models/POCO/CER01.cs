namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing a certificate.
    /// </summary>
    public class CER01
    {
        /// <summary>
        /// Certificate Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Certification Name
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Issuing Organization
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Issue Date
        /// </summary>
        public DateTime? R01F05 { get; set; }
    }
}
