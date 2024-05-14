using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for Skill.
    /// </summary>
    public class DTOSKL01
    {
        /// <summary>
        /// Skill Id
        /// </summary>
        [JsonProperty("L01101")]
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int L01F01 { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [JsonProperty("L01102")]
        [Required(ErrorMessage = "Resume Id is Required")]
        public int UserId { get; set; }

        /// <summary>
        /// Skill Name
        /// </summary>
        [JsonProperty("L01103")]
        [Required(ErrorMessage = "Skill Name is Required")]
        public string L01F03 { get; set; }

        /// <summary>
        /// ProficiencyLevel
        /// </summary>
        [JsonProperty("L01104")]
        [Required(ErrorMessage = "Proficiency Level is Required")]
        public int L01F04 { get; set; }
    }
}
