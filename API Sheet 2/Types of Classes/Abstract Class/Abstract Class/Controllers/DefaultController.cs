using System.Net.Http;
using System.Web.Http;

namespace Abstract_Class.Controllers
{
    /// <summary>
    /// Abstract controller providing a base structure for derived controllers.
    /// </summary>
    public abstract class DefaultController : ApiController
    {
        /// <summary>
        /// Abstract method to be implemented by derived controllers.
        /// </summary>
        /// <returns>HttpResponseMessage representing the result of the action.</returns>
        public abstract HttpResponseMessage Index();
    }
}
