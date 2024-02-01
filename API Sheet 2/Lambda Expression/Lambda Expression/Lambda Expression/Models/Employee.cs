using System.Collections.Generic;

namespace Lambda_Expression.Models
{
    /// <summary>
    /// Represents an employee with basic details.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the department in which the employee works.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        public long Salary { get; set; }

        
    }
}
