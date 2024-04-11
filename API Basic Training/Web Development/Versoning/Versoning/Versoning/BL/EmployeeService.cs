using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Versoning.Models;

namespace Versoning.BL
{
    /// <summary>
    /// Employee Service Class.
    /// </summary>
    public class EmployeeService
    {
        // Sample employee data
        private List<Employee> lstEmp = new List<Employee>()
        {
            new Employee() { Id = 1, Name= "Dimple", Age = 23 },
            new Employee() { Id = 2, Name = "Pankaj", Age = 28 },
            new Employee() { Id = 3, Name = "Abc", Age = 22 },
            new Employee() { Id = 4, Name = "Xyz", Age = 21 },
        };

        /// <summary>
        /// Gets a list of employees.
        /// </summary>
        /// <returns>An IEnumerable of Employee representing the list of employees.</returns>
        public List<Employee> GetAllEmployees()
        {
            return lstEmp;
        }

        /// <summary>
        /// Gets an employee from version 2 of the API.
        /// </summary>
        /// <returns>An Employee representing the employee information.</returns>
        public Employee GetEmployee()
        {
            // Filter employees based on the condition (Id >= 2)
            List<Employee> lstEmp2 = lstEmp.Where(emp => emp.Id >= 2).ToList();

            // Get the first employee from the filtered list
            var objEmp = lstEmp.FirstOrDefault(s => s.Id >= 2);

            return objEmp;
        }
    }
}