using System;
using System.Web.Http;
using Versoning.BL;
using Versoning.Models;

namespace Versoning.Controllers
{
    /// <summary>
    /// Controller for managing version 2 of employee-related operations.
    /// </summary>
    public class EmployeeV2Controller : ApiController
    {
        /// <summary>
        /// Gets an employee from version 2 of the API.
        /// </summary>
        /// <returns>An Employee representing the employee information.</returns>
        [Route("api/v2/Employee/Get")]
        public Employee GetEmployee()
        {
            try
            {
                EmployeeService objEmp = new EmployeeService();
                return objEmp.GetEmployee();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
