using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sealed_Class.Controllers
{
    /// <summary>
    /// Controller that is sealed and inherits from the BaseController.
    /// </summary>
    public class SealedController : BaseController
    {
        /// <summary>
        /// Action method in the sealed controller.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the SealedAction.</returns>
        [HttpGet]
        [Route("api/SealedAction")]
        public HttpResponseMessage SealedAction()
        {
            if (Request == null)
            {
                // Handling the case where the request is null.
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Request is null."),
                    ReasonPhrase = "Internal Server Error"
                };
            }

            return Request.CreateResponse(HttpStatusCode.OK, "In Sealed Controller => Sealed Action");
        }
    }
}
