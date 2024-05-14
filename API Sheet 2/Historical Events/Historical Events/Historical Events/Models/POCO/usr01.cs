using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historical_Events.Models
{
    [Alias("usr01")]
    public class usr01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [PrimaryKey]
        public int r01f01 { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string r01f02 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string r01f03 { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string r01f04 { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        public string r01f05 { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string r01f06 { get; set; }  

        /// <summary>
        /// Roles
        /// </summary>
        public string r01f07 { get; set; }

        [IgnoreOnUpdate]
        public DateTime CreatedAt { get; set; }

        [IgnoreOnInsert]
        public DateTime? UpdatedAt { get; set; }
    }
}