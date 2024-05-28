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
        /// <example>dimplemithiya@gmail.com</example>
        [Required(ErrorMessage = "Email Id Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>abc123</example>
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
