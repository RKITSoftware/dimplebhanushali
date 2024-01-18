using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sealed_Class.Controllers
{
    /// <summary>
    /// Base controller with a single action method.
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// Action method in the base controller.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the BaseAction.</returns>
        [HttpGet]
        [Route("api/BaseAction")]
        public HttpResponseMessage BaseAction()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "In Base Controller => Base Action");
        }
    }
}
