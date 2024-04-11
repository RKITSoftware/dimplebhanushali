using Filters.Filters.Asynchronous_Filter;
using Microsoft.AspNetCore.Mvc;

namespace Filters.Controllers
{
    [ServiceFilter(typeof(LogAsyncActionFilter))]
    public class AsyncController : Controller
    {
        [Route("Async")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
