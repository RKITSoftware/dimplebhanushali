namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Education Model
    /// </summary>
    public class EDU01
    {
        /// <summary>
        /// Education Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int U01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Institution
        /// </summary>
        public string U01F03 { get; set; }

        /// <summary>
        /// Degree
        /// </summary>
        public string U01F04 { get; set; }

        /// <summary>
        /// Field of Study
        /// </summary>
        public string U01F05 { get; set; }

        /// <summary>
        /// Education Year
        /// </summary>
        public int U01F06 { get; set; }
    }
}
