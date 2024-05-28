using Microsoft.AspNetCore.Mvc;

namespace Routing_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLRoutingController : ControllerBase
    {
        /// <summary>
        /// GET: api/CLRouting/Greetings
        /// </summary>
        /// <param name="name">The name parameter.</param>
        /// <returns>An IActionResult containing a welcome message.</returns>
        //[HttpGet("Greetings")]
        public IActionResult Greeting(string name)
        {
            return Ok($"Welcome To Routing Controller {name}");
        }
    }
}
