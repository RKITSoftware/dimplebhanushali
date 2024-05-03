using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Experience Model
    /// </summary>
    public class DTOEXP01
    {
        /// <summary>
        /// Experiance Id
        /// </summary>
        [JsonProperty("P01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("P01102")]
        [Required(ErrorMessage = "Resume id is Required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        [JsonProperty("P01103")]
        [Required(ErrorMessage = "Company is Required.")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        [JsonProperty("P01104")]
        [Required(ErrorMessage = "Position is Required.")]
        public string P01F04 { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        [JsonProperty("P01105")]
        [Required(ErrorMessage = "StartDate is Required.")]
        [DataType(DataType.Date)]
        public DateTime P01F05 { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        [JsonProperty("P01106")]
        [Required(ErrorMessage = "EndDate is Required.")]
        [DataType(DataType.Date)]
        public DateTime P01F06 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("P01107")]
        [Required(ErrorMessage = "Description is Required.")]
        public string P01F07 { get; set; }
    }
}
