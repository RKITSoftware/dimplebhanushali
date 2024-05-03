using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    public class DTORES01
    {
        /// <summary>
        /// Resume Id
        /// </summary>
        [JsonProperty("S01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int S01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("S01102")]
        [Required(ErrorMessage = "User Id is Required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("S01103")]
        [Required(ErrorMessage = "Title is Required.")]
        public string S01F03 { get; set; }

        /// <summary>
        /// Summary
        /// </summary>
        [JsonProperty("S01104")]
        [Required(ErrorMessage = "Summary is Required.")]
        public string S01F04 { get; set; }
    }
}
