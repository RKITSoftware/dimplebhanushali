using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// Project Model
    /// </summary>
    public class PRO01
    {
        /// <summary>
        /// Project Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int O01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Project Name
        /// </summary>
        public string O01F03 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string O01F04 { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime? O01F05 { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime? O01F06 { get; set; }
    }
}
