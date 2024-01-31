using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Versoning.Models
{
    /// <summary>
    /// Represents an employee with basic details.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the employee.
        /// </summary>
        public int Age { get; set; }
    }
}
