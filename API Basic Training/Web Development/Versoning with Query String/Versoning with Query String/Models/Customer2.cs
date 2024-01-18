using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Versoning_with_Query_String.Models
{
    /// <summary>
    /// Represents a customer entity for API version 2.
    /// </summary>
    public class Customer2
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public long PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the gender of the customer.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// List of sample customers for version 2.
        /// </summary>
        public static List<Customer2> lstCustomers2 = new List<Customer2>
        {
            new Customer2() {Id=1, Name="Dimple", PhoneNumber=9624863508, Gender="Female"},
            new Customer2() {Id=2, Name="Ankit", PhoneNumber=9619717709, Gender="Male"},
            new Customer2() {Id=3, Name="Pankaj", PhoneNumber=9988774455, Gender="Male"},
            new Customer2() {Id=4, Name="Krishna", PhoneNumber=9966550022, Gender="Female"}
        };
    }
}
