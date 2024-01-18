using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sealed_Class.Controllers
{
    /// <summary>
    /// Controller for demonstrating the attempt to create an instance of a sealed controller.
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Action method that attempts to create an instance of a sealed controller and call a sealed action.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the action.</returns>
        [HttpGet]
        [Route("api/CallSealedAction")]
        public HttpResponseMessage CallSealedAction()
        {
            // Sealed classes cannot be instantiated, so this line will result in a compilation error
            SealedController objSealed = new SealedController();

            // You need to call the static method or access static members directly from the sealed class
            return objSealed.SealedAction();
        }
    }
}
