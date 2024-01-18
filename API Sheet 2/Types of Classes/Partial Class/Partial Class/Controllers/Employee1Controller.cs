using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Partial_Class.Controllers
{
    /// <summary>
    /// Partial class for managing employee-related actions.
    /// </summary>
    public partial class EmployeeController : ApiController
    {
        /// <summary>
        /// Action method to get an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be retrieved.</param>
        /// <returns>HttpResponseMessage representing the result of the GetId action.</returns>
        [HttpGet]
        [Route("api/{id}")]
        public HttpResponseMessage GetId(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, $"Id => {id}");
        }
    }
}
