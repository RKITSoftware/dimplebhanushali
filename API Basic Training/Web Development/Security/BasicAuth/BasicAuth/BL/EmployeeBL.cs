using BasicAuth.Models;
using System.Collections.Generic;
using System.Linq;

namespace BasicAuth.BL
{
    /// <summary>
    /// Business logic class for employee operations.
    /// </summary>
    public class EmployeeBL
    {
        // Sample list of employees (for demonstration purposes)
        private List<Employee> EmployeeList = new List<Employee>
        {
            new Employee {ID = 1 , Name ="Dimple" , City= "London", IsActive = true},
            new Employee {ID = 2 , Name ="Ankit" , City= "Mumbai", IsActive = false}
        };

        /// <summary>
        /// Retrieves the list of all employees.
        /// </summary>
        /// <returns>The list of employees.</returns>
        public List<Employee> GetEmployees()
        {
            return EmployeeList;
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee with the specified ID, or null if not found.</returns>
        public Employee GetEmployeeById(int id)
        {
            return EmployeeList.FirstOrDefault(e => e.ID == id);
        }
    }
}
