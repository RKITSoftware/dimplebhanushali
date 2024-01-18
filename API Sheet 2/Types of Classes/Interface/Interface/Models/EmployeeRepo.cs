using Interface.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Interface.Models
{
    /// <summary>
    /// Repository for managing employee data.
    /// </summary>
    public class EmployeeRepo : IEmployee<Employee>
    {
        // Static list to store employee data
        public static List<Employee> lstEmployees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Dimple Bhanushali", Department = "Development" },
            new Employee { Id = 2, Name = "Ankit Bhanushali", Department = "Analysis" }
        };

        /// <summary>
        /// Adds a new employee to the repository.
        /// </summary>
        /// <param name="objEmp">The employee object to be added.</param>
        public void Add(Employee objEmp)
        {
            objEmp.Id = lstEmployees.Count + 1;
            lstEmployees.Add(objEmp);
        }

        /// <summary>
        /// Deletes an employee from the repository by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be deleted.</param>
        public void Delete(int id)
        {
            lstEmployees.Remove(lstEmployees.FirstOrDefault(emp => emp.Id == id));
        }

        /// <summary>
        /// Retrieves all employees from the repository.
        /// </summary>
        /// <returns>A collection of all employee objects.</returns>
        public IEnumerable<Employee> GetAll()
        {
            return lstEmployees;
        }

        /// <summary>
        /// Retrieves an employee from the repository by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be retrieved.</param>
        /// <returns>The employee object.</returns>
        public Employee GetById(int id)
        {
            return lstEmployees.FirstOrDefault(emp => emp.Id == id);
        }

        /// <summary>
        /// Updates an existing employee in the repository.
        /// </summary>
        /// <param name="id">The ID of the employee to be updated.</param>
        /// <param name="objEmp">The updated employee object.</param>
        public void Update(int id, Employee objEmp)
        {
            Employee objEmpEditable = lstEmployees.FirstOrDefault(emp => emp.Id == id);
            objEmpEditable.Name = objEmp.Name;
            objEmpEditable.Department = objEmp.Department;
        }
    }
}
