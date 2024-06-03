using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MiddlewareWithFilters.Models.DTO
{
    /// <summary>
    /// Model Reoresenting DTO Model for User.
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        [JsonProperty("R01101")]
        [Required(ErrorMessage = "Id is Required.")]
        [Range(0, int.MaxValue)]
        public int R01F01 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty("R01102")]
        [Required(ErrorMessage = "First Name is Required.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty("R01103")]
        [Required(ErrorMessage = "Last Name is Required.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [JsonProperty("R01104")]
        [ServiceStack.DataAnnotations.Unique]
        [Required(ErrorMessage = "User Name is Required.")]
        public string R01F04 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("R01105")]
        [Required(ErrorMessage = "Password is Required.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string R01F05 { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("R01106")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email id is Required.")]
        public string R01F06 { get; set; }
    }
}
