using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for Projects.
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Project Id
        /// </summary>
        [JsonProperty("O01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int O01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("O01102")]
        [Required(ErrorMessage = "Resume Id is Required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Project Name
        /// </summary>
        [JsonProperty("O01103")]
        [Required(ErrorMessage = "Project Name is Required.")]
        public string O01F03 { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("O01104")]
        [Required(ErrorMessage = "Description is Required.")]
        public string O01F04 { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        [JsonProperty("O01105")]
        [Required(ErrorMessage = "Start Date is Required.")]
        [DataType(DataType.DateTime)]
        public DateTime O01F05 { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        [JsonProperty("O01106")]
        [Required(ErrorMessage = "End Date is Required.")]
        [DataType(DataType.DateTime)]
        public DateTime O01F06 { get; set; }
    }
}
