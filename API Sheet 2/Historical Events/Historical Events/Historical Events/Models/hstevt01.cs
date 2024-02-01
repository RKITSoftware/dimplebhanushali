using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historical_Events.Models
{
    [Table("hstevt01")]
    public class hstevt01
    {
        [PrimaryKey]
        [Column("t01f01")]
        public int t01f01 { get; set; }

        [Column("t01f02")]
        public int t01f02 { get; set; }

        [Column("t01f03")]
        public string t01f03 { get; set; }

        [Column("t01f04")]
        public string t01f04 { get; set; }
    }
}