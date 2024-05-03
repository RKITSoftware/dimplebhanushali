using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Language Model
    /// </summary>
    public class LAN01
    {
        /// <summary>
        /// Language Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement] 
        public int N01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Language Name
        /// </summary>
        public string N01F03 { get; set; }

        /// <summary>
        /// Proficiency Level
        /// </summary>
        public int N01F04 { get; set; }
    }
}
