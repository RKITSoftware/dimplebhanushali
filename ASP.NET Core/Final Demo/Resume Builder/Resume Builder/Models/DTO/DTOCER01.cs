using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    public class DTOCER01
    {
        /// <summary>
        /// Certificate Id
        /// </summary>
        [JsonProperty("R01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("R01102")]
        [Required(ErrorMessage = "Resume Id is Required.")]
        public int UserId { get; set; }

        /// <summary>
        /// Certification Name
        /// </summary>
        [JsonProperty("R01103")]
        [Required(ErrorMessage = "Certificate Name is Required.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Issuing Organization
        /// </summary>
        [JsonProperty("R01104")]
        [Required(ErrorMessage = "Isuue Organization is Required.")]
        public string R01F04 { get; set; }

        /// <summary>
        /// Issue Date
        /// </summary>
        [JsonProperty("R01105")]
        [Required(ErrorMessage = "Issue Date is Required.")]
        [DataType(DataType.Date)]
        public DateTime? R01F05 { get; set; }
    }
}
