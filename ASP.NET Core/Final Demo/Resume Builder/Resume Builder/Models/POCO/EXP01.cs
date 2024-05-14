namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing a Experience.
    /// </summary>
    public class EXP01
    {
        /// <summary>
        /// Experiance Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string P01F03 { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime P01F05 { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime P01F06 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string P01F07 { get; set; }
    }
}
