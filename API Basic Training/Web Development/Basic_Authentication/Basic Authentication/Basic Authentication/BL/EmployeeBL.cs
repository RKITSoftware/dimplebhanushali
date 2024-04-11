using Basic_Authentication.Models;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Authentication.BL
{
    /// <summary>
    /// Class for Performing Crud for Students.
    /// </summary>
    public class EmployeeBL
    {
        /// <summary>
        /// Generate Employee List For Testing.
        /// </summary>
        public static List<Employee> EmpList = new List<Employee>
        {
            new Employee {Id = 1 , FirstName ="Dimple" , LastName="Mithiya", City="Rajkot"},
            new Employee {Id = 2 , FirstName ="Ankit" , LastName="Katarmal", City="London"},
            new Employee {Id = 3 , FirstName ="Ram" , LastName="Krishna", City="Ahmedabad"}
        };

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>List of all employees.</returns>
        public List<Employee> GetAllEmployees()
        {
            return EmpList.ToList(); // Return a copy to prevent modification of the original list
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee with the specified ID or null if not found.</returns>
        public Employee GetEmployeeById(int id)
        {
            return EmpList.FirstOrDefault(emp => emp.Id == id);
        }
    }
}
