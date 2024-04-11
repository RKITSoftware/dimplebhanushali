using Exception_Demo.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Exception_Demo.Controllers
{
    /// <summary>
    /// Custom Controller for Exception
    /// </summary>
    public class CustomerController : ApiController
    {
        /// <summary>
        /// Throws an InvalidOperationException for testing exception handling.
        /// </summary>
        [HttpGet]
        [Route("api/Error")]
        public IHttpActionResult ThrowError()
        {
            throw new InvalidOperationException("This is a Thrown Exception.");
        }
    }
}
