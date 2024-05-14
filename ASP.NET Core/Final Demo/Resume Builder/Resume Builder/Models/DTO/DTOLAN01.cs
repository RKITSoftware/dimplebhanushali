using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for Language.
    /// </summary>
    public class DTOLAN01
    {
        /// <summary>
        /// Language Id
        /// </summary>
        [JsonProperty("N01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int N01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("N01102")]
        [Required(ErrorMessage = "Resume Id is Required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Language Name
        /// </summary>
        [JsonProperty("N01103")]
        [Required(ErrorMessage = "Resume Id is Required.")]
        public string N01F03 { get; set; }

        /// <summary>
        /// Proficiency Level
        /// </summary>
        [JsonProperty("N01104")]
        [Required(ErrorMessage = "Profiency Level is Required.")]
        public int N01F04 { get; set; }
    }
}
