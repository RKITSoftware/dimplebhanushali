using Basic_Autthnetication_Authorization.Authorization_Provider;
using Basic_Autthnetication_Authorization.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Basic_Autthnetication_Authorization.Controllers
{
    /// <summary>
    /// Controller for managing Employee-related actions.
    /// </summary>
    [RoutePrefix("api/Employees")]
    [BasicAuthenticationAttribute]
    public class EmployeeController : ApiController
    {
        #region OneEmployeeDetails
        /// <summary>
        /// Gets details of a single Employee based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the Employee.</param>
        /// <returns>HTTP response with the Employee details if found, otherwise returns NotFound.</returns>
        [HttpGet]
        [Route("OneEmployeeDetails/{id}")]
        [BasicAuthorizationAttribute(Roles = "User,Admin,SuperAdmin")]
        public HttpResponseMessage OneEmployeeDetails(int id)
        {
            // Gets the Employee by ID.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.EmployeesDetail().FirstOrDefault(s => s.Id == id));

        }
        #endregion

        #region FewEmployeesDetails

        /// <summary>
        /// Gets details of a few Employees, intended for authorized users with the role Admin.
        /// </summary>
        /// <returns>HTTP response with details of selected Employees.</returns>
        [HttpGet]
        [Route("FewEmployeesDetails")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        public HttpResponseMessage FewEmployeesDetails()
        {
            // Retrieves details of Employees with ID less than 5.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.EmployeesDetail().Where(s => s.Id < 5));
        }

        #endregion

        #region AllEmployeesDetails

        /// <summary>
        /// Gets details of all Employees, intended for authorized users with the role SuperAdmin.
        /// </summary>
        /// <returns>HTTP response with details of all Employees.</returns>
        [HttpGet]
        [Route("AllEmployeesDetails")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public HttpResponseMessage AllEmployeesDetails()
        {
            // Retrieves details of all Employees.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.EmployeesDetail());
        }
        #endregion

    }
}
