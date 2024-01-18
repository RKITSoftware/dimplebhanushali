using Partial_Class.Models;
using System.Web.Http;

namespace Partial_Class.Controllers
{
    /// <summary>
    /// Partial class for managing employee-related actions.
    /// </summary>
    public partial class EmployeeController : ApiController
    {
        /// <summary>
        /// Action method to add an employee.
        /// </summary>
        /// <param name="objEmp">The employee object to be added.</param>
        /// <returns>ActionResult representing the result of the AddEmployee action.</returns>
        [HttpPost]
        [Route("api/AddEmployee")]
        public IHttpActionResult AddEmployee([FromBody] Employee objEmp)
        {
            return Ok(objEmp);
        }
    }
}
