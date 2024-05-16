using System.Collections.Generic;

namespace Basic_Autthnetication_Authorization.Models
{
    /// <summary>
    /// Represents a Employee Model with authentication and authorization information.
    /// </summary>
    public class Employee
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the student.
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a list of student details for demonstration purposes.
        /// </summary>
        /// <returns>A list of student objects.</returns>
        public static List<Employee> EmployeesDetail()
        {
            List<Employee> lstEmployees = new List<Employee>
            {
                new Employee{Id = 1, FirstName = "Dimple", LastName = "Mithiya", Email = "dimple@gmail.com"},
                new Employee{Id = 2, FirstName = "Krishna", LastName = "Hare", Email = "abc@gmail.com"},
                new Employee{Id = 3, FirstName = "Ankit", LastName = "Bhanushali", Email = "ankit@gmail.com"},
                new Employee{Id = 4, FirstName = "Dimple", LastName = "Mithiya", Email = "dimple@gmail.com"},
                new Employee{Id = 5, FirstName = "Krishna", LastName = "Hare", Email = "abc@gmail.com"},
                new Employee{Id = 6, FirstName = "Ankit", LastName = "Bhanushali", Email = "ankit@gmail.com"},
                new Employee{Id = 7, FirstName = "Dimple", LastName = "Mithiya", Email = "dimple@gmail.com"},
                new Employee{Id = 8, FirstName = "Krishna", LastName = "Hare", Email = "abc@gmail.com"},
                new Employee{Id = 9, FirstName = "Ankit", LastName = "Bhanushali", Email = "ankit@gmail.com"},
            };
            return lstEmployees;
        }
        #endregion

    }
}