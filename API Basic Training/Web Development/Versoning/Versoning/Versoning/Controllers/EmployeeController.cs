using System;
using System.Collections.Generic;
using System.Web.Http;
using Versoning.BL;
using Versoning.Models;

namespace Versoning.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Gets a list of employees.
        /// </summary>
        /// <returns>An IEnumerable of Employee representing the list of employees.</returns>
        [Route("api/v1/Employee/Get")]
        public IEnumerable<Employee> GetAllEMployees()
        {
            try
            {
                EmployeeService objEmp = new EmployeeService();
                return objEmp.GetAllEmployees();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
