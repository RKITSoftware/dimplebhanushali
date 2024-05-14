using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Historical_Events.Models.DTO
{
    public class DTOUSR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("r01101")]
        [Required(ErrorMessage = "User id is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid user id")]
        public int r01f01 { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        [JsonProperty("r01102")]
        [Required(ErrorMessage = "Full name is required")]
        public string r01f02 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [JsonProperty("r01103")]
        [Required(ErrorMessage = "Username is required")]
        public string r01f03 { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("r01104")]
        [Required(ErrorMessage = "Emailid is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email id")]
        public string r01f04 { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [JsonProperty("r01105")]
        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number")]
        public string r01f05 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [JsonProperty("r01106")]
        [Required(ErrorMessage = "Password is required")]
        public string r01f06 { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        [JsonProperty("r01107")]
        [Required(ErrorMessage ="Role is required")]
        public string r01f07 { get; set; }
    }
}