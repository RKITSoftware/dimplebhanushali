namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing a Resume.
    /// </summary>
    public class RES01
    {
        /// <summary>
        /// Resume Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement] 
        public int S01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Template Id
        /// </summary>
        public int S01F03 { get; set; }

    }
}
