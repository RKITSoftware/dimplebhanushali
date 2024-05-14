namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing a Skill.
    /// </summary>
    public class SKL01
    {
        /// <summary>
        /// Skill Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement] 
        public int L01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Skill Name
        /// </summary>
        public string L01F03 { get; set; }

        /// <summary>
        /// ProficiencyLevel
        /// </summary>
        public int L01F04 { get; set; }
    }
}
