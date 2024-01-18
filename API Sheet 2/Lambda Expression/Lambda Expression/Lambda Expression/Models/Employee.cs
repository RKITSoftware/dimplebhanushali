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

        /// <summary>
        /// A static list of employees for demonstration purposes.
        /// </summary>
        public static List<Employee> lstEmployees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Dimple Mithiya", Department = "Development", Salary = 50000 },
            new Employee { Id = 2, Name = "Pankaj Mithiya", Department = "Business", Salary = 75000 },
            new Employee { Id = 3, Name = "Ankit Bhanushali", Department = "Business Analytics", Salary = 80000 }
        };
    }
}
