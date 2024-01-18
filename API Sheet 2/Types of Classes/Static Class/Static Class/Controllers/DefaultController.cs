using Static_Class.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Static_Class.Controllers
{
    /// <summary>
    /// Controller for generating a new unique ID.
    /// </summary>
    public class DefaultController : ApiController
    {
        /// <summary>
        /// Action method to get a new unique ID.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the GetUniqueId action.</returns>
        [HttpGet]
        [Route("api/Id")]
        public HttpResponseMessage GetUniqueId()
        {
            // Generate a new unique ID using the static method from the UniqueId class
            return Request.CreateResponse(HttpStatusCode.OK, $"New Unique Id => {UniqueId.GenerateUniqueId()}");
        }
    }
}
