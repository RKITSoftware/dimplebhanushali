using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Abstract_Class.Controllers
{
    /// <summary>
    /// Concrete controller derived from DefaultController.
    /// </summary>
    public class ValuesController : DefaultController
    {
        /// <summary>
        /// Implementation of the Index action.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the Index action.</returns>
        [HttpGet]
        [Route("api/Index")]
        public override HttpResponseMessage Index()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "In Values Controller");
        }

        /// <summary>
        /// Additional action named Home.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the Home action.</returns>
        [HttpGet]
        [Route("api/Home")]
        public HttpResponseMessage Home()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "This is Home Page");
        }
    }
}
