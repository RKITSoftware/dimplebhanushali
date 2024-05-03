using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models
{
    /// <summary>
    /// Credential
    /// </summary>
    public class CRE01
    {
        /// <summary>
        /// Email Id
        /// </summary>
        [Required(ErrorMessage = "Email Id Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
