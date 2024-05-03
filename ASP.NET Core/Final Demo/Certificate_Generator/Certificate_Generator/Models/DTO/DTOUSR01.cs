using Newtonsoft.Json;

namespace Certificate_Generator.Models.DTO
{
    public class DTOUSR01
    {
        ///<summary>
        /// User Id
        /// </summary>
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public string R01F05 { get; set; }

    }
}
