using Basic_Authentication.Authorisation_Provider;
using Basic_Authentication.BL;
using System.Web.Http;
using System.Web.Routing;

namespace Basic_Authentication.Controllers
{
    /// <summary>
    /// API Controller for managing employee data.
    /// </summary>
    public class EmployeeController : ApiController
    {
        private static EmployeeBL _employee;

        /// <summary>
        /// Static constructor to initialize the EmployeeBL instance.
        /// </summary>
        static EmployeeController()
        {
            _employee = new EmployeeBL();
        }

        /// <summary>
        /// Gets all employees with admin role authorization.
        /// </summary>
        /// <returns>List of all employees.</returns>
        [HttpGet]
        [Authorisation(Roles = "admin")]
        [Route("api/Get")]
        public IHttpActionResult GetAllEmployees()
        {
            return Ok(_employee.GetAllEmployees());
        }

        /// <summary>
        /// Gets an employee by ID with employee role authorization.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee with the specified ID or a BadRequest if the ID is null.</returns>
        [HttpGet]
        [Authorisation(Roles = "employee")]
        [Route("api/GetbyId/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            return Ok(_employee.GetEmployeeById(id));
        }
    }
}
