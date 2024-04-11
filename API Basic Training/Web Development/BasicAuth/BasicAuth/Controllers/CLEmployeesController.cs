using BasicAuth.Auth;
using BasicAuth.BL;
using BasicAuth.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace BasicAuth.Controllers
{
    /// <summary>
    /// Controller for handling employee-related operations with basic authentication.
    /// </summary>
    [BasicAuthentication]
    public class CLEmployeesController : ApiController
    {
        private EmployeeBL employeeBL = new EmployeeBL(); 

        /// <summary>
        /// Retrieves the list of all employees.
        /// </summary>
        /// <returns>The list of employees.</returns>
        [HttpGet, Route("api/Get")]
        [BasicAuthorization(Roles = "Admin,Employee")]
        public IHttpActionResult Get()
        {
            List<Employee> employees = employeeBL.GetEmployees();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves the employee with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee with the specified ID if found; otherwise, NotFound response.</returns>
        // GET: api/Employees/1
        [HttpGet, Route("api/get/{id}")]
        [BasicAuthorization(Roles = "Admin")]
        public IHttpActionResult Get(int id)
        {
            var employee = employeeBL.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
