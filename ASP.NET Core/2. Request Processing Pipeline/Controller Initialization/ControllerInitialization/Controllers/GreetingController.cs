using ControllerInitialization.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerInitialization.Controllers
{ 
    /// <summary>
    /// Controller for greeting
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        //instance of IGreetingService
        private readonly IGreetingService _greetingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreetingController"/> class.
        /// </summary>
        /// <param name="greetingService">The greeting service.</param>
        public GreetingController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        /// <summary>
        /// Gets a greeting message.
        /// </summary>
        /// <param name="name">The name to greet.</param>
        /// <returns>A greeting message.</returns>
        [HttpGet("{name}")]
        public IActionResult GetGreeting(string name)
        {
            string message = _greetingService.Greet(name);
            return Ok(message);
        }

    }
}
