using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historical_Events.Models
{
    [Table("usr01")]
    public class usr01
    {
        [PrimaryKey]
        public int r01f01 { get; set; }

        public string r01f02 { get; set; }

        public string r01f03 { get; set; }

        public string r01f04 { get; set; }  

        public string r01f05 { get; set; }
    }
}