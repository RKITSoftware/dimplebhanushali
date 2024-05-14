using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for User.
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("R01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// First Name of User
        /// </summary>
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Last Name of User
        /// </summary>
        [Required(ErrorMessage = "Last Name is Required.")]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Email Id of User
        /// </summary>
        [Required(ErrorMessage = "Email Id is Required.")]
        [DataType(DataType.EmailAddress)]
        [JsonProperty("R01104")]
        public string R01F04 { get; set; }

        /// <summary>
        /// Mobile Number of User
        /// </summary>
        [Required(ErrorMessage = "Mobile Number is Required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid Indian Mobile Number.")]
        [JsonProperty("R01105")]
        public string R01F05 { get; set; }

        /// <summary>
        /// Age of User
        /// </summary>
        [Range(15, 100, ErrorMessage = "Age must be between 15 and 100.")]
        [JsonProperty("R01106")]
        public int R01F06 { get; set; }
 
        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("R01107")]
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string R01F07 { get; set; }
    }
}
