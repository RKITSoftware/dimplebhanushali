using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Versoning_with_Query_String.Models
{
    /// <summary>
    /// Represents a customer entity for API version 1.
    /// </summary>
    public class Customer1
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FistName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public long PhoneNumber { get; set; }

        /// <summary>
        /// List of sample customers for version 1.
        /// </summary>
        public static List<Customer1> lstCustomers1 = new List<Customer1>
        {
            new Customer1() {Id=1, FistName="Dimple", LastName="Mithiya", PhoneNumber=9624863508},
            new Customer1() {Id=2, FistName="Ankit", LastName="Katarmal", PhoneNumber=9619717709},
            new Customer1() {Id=3, FistName="Pankaj", LastName="Mithiya", PhoneNumber=9988774455},
            new Customer1() {Id=4, FistName="Krishna", LastName="Mithiya", PhoneNumber=9966550022},
        };
    }
}
