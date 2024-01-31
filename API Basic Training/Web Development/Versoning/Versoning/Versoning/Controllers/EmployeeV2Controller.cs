using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Versoning.Models;

namespace Versoning.Controllers
{
    /// <summary>
    /// Controller for managing version 2 of employee-related operations.
    /// </summary>
    public class EmployeeV2Controller : ApiController
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
        /// Gets an employee from version 2 of the API.
        /// </summary>
        /// <returns>An Employee representing the employee information.</returns>
        [Route("api/v2/Employee/Get")]
        public Employee Get()
        {
            // Filter employees based on the condition (Id >= 2)
            List<Employee> lstEmp2 = lstEmp.Where(emp => emp.Id >= 2).ToList();

            // Get the first employee from the filtered list
            var objEmp = lstEmp.FirstOrDefault(s => s.Id >= 2);

            return objEmp;
        }
    }
}
