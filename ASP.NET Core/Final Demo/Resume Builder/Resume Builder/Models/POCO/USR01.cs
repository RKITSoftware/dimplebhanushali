using System.ComponentModel.DataAnnotations;

namespace Resume_Builder.Models.POCO
{
    /// <summary>
    /// User Model Class representing Properties of User.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [ServiceStack.DataAnnotations.PrimaryKey, ServiceStack.DataAnnotations.AutoIncrement]
        public int R01F01 { get; set; }

        /// <summary>
        /// First Name of User
        /// </summary>
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Last Name of User
        /// </summary>
        [Required(ErrorMessage = "Last Name is Required.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Email Id of User
        /// </summary>
        [Required(ErrorMessage = "Email Id is Required.")]
        [DataType(DataType.EmailAddress)]
        public string R01F04 { get; set; }

        /// <summary>
        /// Mobile Number of User
        /// </summary>
        [Required(ErrorMessage = "Mobile Number is Required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid Indian Mobile Number.")]
        public string R01F05 { get; set; }

        /// <summary>
        /// Age of User
        /// </summary>
        [Range(15, 100, ErrorMessage = "Age must be between 15 and 100.")]
        public int R01F06{ get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is Required.")]
        [DataType(DataType.Password)]
        public string R01F07 { get; set; }

        /// <summary>
        /// User Created Date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? R01F08 { get; set; }

        /// <summary>
        /// User Updated Date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? R01F09 { get; set; }
    }
}
