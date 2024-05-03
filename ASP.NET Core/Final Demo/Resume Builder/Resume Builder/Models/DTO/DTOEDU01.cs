using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    public class DTOEDU01
    {
        /// <summary>
        /// Education Id
        /// </summary>
        [JsonProperty("U01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int U01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("U01102")]
        [Required(ErrorMessage = " Resume Id is required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Institution
        /// </summary>
        [JsonProperty("U01103")]
        [Required(ErrorMessage = " Institution Id is required.")]
        public string U01F03 { get; set; }

        /// <summary>
        /// Degree
        /// </summary>
        [JsonProperty("U01104")]
        [Required(ErrorMessage = " Degree is required.")]
        public string U01F04 { get; set; }

        /// <summary>
        /// Field of Study
        /// </summary>
        [JsonProperty("U01105")]
        [Required(ErrorMessage = "Filed of Study/ Department is required.")]
        public string U01F05 { get; set; }

        /// <summary>
        /// Education Year
        /// </summary>
        [JsonProperty("U01106")]
        [Required(ErrorMessage = "Education year is required.")]
        public int U01F06 { get; set; }
    }
}
