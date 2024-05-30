using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers
{
    public class AttributeRoutingController : Controller
    {
        [Route("Attribute")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
