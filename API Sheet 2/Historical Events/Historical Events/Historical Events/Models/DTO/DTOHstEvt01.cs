using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Historical_Events.Models.DTO
{
    /// <summary>
    /// Entity Representing DTO Model for HIstorical Events.
    /// </summary>
    public class DTOHstEvt01
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("t01101")]
        [Required(ErrorMessage = "Id required")]
        public int t01f01 { get; set; }

        /// <summary>
        /// Date of News Headline
        /// </summary>
        [JsonProperty("t01102")]
        [Required(ErrorMessage = "Date Required")]
        public int t01f02 { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        [JsonProperty("t01103")]
        [Required(ErrorMessage = "Tag required")]
        public string t01f03 { get; set; }

        /// <summary>
        /// News Headline
        /// </summary>
        [JsonProperty("t01104")]
        [Required(ErrorMessage = "Headline required")]
        public string t01f04 { get; set; }

        /// <summary>
        /// Views
        /// </summary>
        [JsonProperty("t01105")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid value of view")]
        [Required(ErrorMessage ="Views required")]
        public int t01f05 { get; set; }

    }
}