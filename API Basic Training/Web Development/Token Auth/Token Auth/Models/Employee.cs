using System.Collections.Generic;

namespace Token_Auth.Models
{
    /// <summary>
    /// Represents an employee with basic information.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender of the employee.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// List of sample employees.
        /// </summary>
        public static List<Employee> lstEmployees = new List<Employee>
        {
            new Employee { Id=1, FirstName="Dimple", LastName="Mithiya", Email="abc@gmail.com", Gender="Female"},
            new Employee { Id=2, FirstName="Ankit", LastName="Katarmal", Email="xyz@gmail.com", Gender="Male"},
            new Employee { Id=3, FirstName="Shiva", LastName="Shakti", Email="shiva@gmail.com", Gender="Male"},
        };
    }
}
