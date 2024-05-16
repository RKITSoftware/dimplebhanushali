using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historical_Events.Models
{
    [Table("hstevt01")]
    public class HSTEVT01
    {
        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        [Column("t01f01")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Date of News Headline
        /// </summary>
        [Column("t01f02")]
        public int t01f02 { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        [Column("t01f03")]
        public string t01f03 { get; set; }

        /// <summary>
        /// News Headline
        /// </summary>
        [Column("t01f04")]
        public string t01f04 { get; set; }

        /// <summary>
        /// Views
        /// </summary>
        [Column("t01f05")]
        public int t01f05 { get; set; }

    }
}
