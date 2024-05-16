using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Token_Auth.Models;

namespace Token_Auth.Controllers
{
    /// <summary>
    /// Controller for handling employee-related operations with token-based authorization.
    /// </summary>
    public class EmployeeController : ApiController
    {
        /// <summary>
        /// Retrieves an employee by ID with user-level authorization.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>An HTTP response message containing the employee information if authorized; otherwise, an unauthorized response.</returns>
        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("api/emp/{id}")]
        public HttpResponseMessage GetEmployeeById([FromUri] int id)
        {
            // Retrieve the employee by ID
            Employee objEmp = Employee.lstEmployees.FirstOrDefault(x => x.Id == id);

            // Create an HTTP response message with the employee information or an unauthorized response
            return Request.CreateResponse(HttpStatusCode.OK, objEmp);
        }

        /// <summary>
        /// Retrieves some employees with admin-level authorization.
        /// </summary>
        /// <returns>An HTTP response message containing employee information if authorized; otherwise, an unauthorized response.</returns>
        [Authorize(Roles = "admin,superadmin")]
        [Route("api/some")]
        [HttpGet]
        public HttpResponseMessage GetSomeEmployee()
        {
            // Retrieve some employees (first two) based on admin-level authorization
            Employee objEmp = Employee.lstEmployees.FirstOrDefault(x => x.Id <= 2);

            // Create an HTTP response message with the employee information or an unauthorized response
            return Request.CreateResponse(HttpStatusCode.OK, objEmp);
        }

        /// <summary>
        /// Retrieves all employees with superadmin-level authorization.
        /// </summary>
        /// <returns>An HTTP response message containing the list of employees if authorized; otherwise, an unauthorized response.</returns>
        [Authorize(Roles = "superadmin")]
        [Route("api/all")]
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            // Retrieve all employees based on superadmin-level authorization
            List<Employee> lstEmp = Employee.lstEmployees.ToList();

            // Create an HTTP response message with the list of employees or an unauthorized response
            return Request.CreateResponse(HttpStatusCode.OK, lstEmp);
        }
    }
}
