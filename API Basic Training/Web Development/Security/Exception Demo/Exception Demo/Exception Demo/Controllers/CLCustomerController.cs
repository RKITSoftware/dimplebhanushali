using System;
using System.Web.Http;

namespace Exception_Demo.Controllers
{
    /// <summary>
    /// Custom Controller for Exception
    /// </summary>
    public class CLCustomerController : ApiController
    {
        /// <summary>
        /// Throws an InvalidOperationException for testing exception handling.
        /// </summary>
        [HttpGet]
        [Route("api/Exceptiion")]
        public IHttpActionResult InvalidException()
        {
            throw new InvalidOperationException();
        }
        
        /// <summary>
        /// Throws an InvalidOperationException for testing exception handling.
        /// </summary>
        [HttpGet]
        [Route("api/NullExceptiion")]
        public IHttpActionResult NullReferenceException()
        {
            throw new NullReferenceException();
        }
    }
}
