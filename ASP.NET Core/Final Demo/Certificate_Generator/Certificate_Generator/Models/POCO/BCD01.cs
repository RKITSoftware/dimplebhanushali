using ServiceStack.DataAnnotations;

namespace Certificate_Generator.Models.POCO
{
    /// <summary>
    /// Bulk Certificate Data
    /// </summary>
    public class BCD01
    {
        /// <summary>
        /// Bulk Id
        /// </summary>
        [AutoIncrement, PrimaryKey]
        public int D01F01 { get; set; }

        /// <summary>
        /// Job Id
        /// </summary>
        public int D01F02 { get; set; }

        /// <summary>
        /// Certificate Id
        /// </summary>
        public int D01F03 { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime D01F04 { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [IgnoreOnInsert]
        public DateTime D01F05 { get; set; }

    }
}
