using Microsoft.AspNetCore.Mvc;
using Middleware_API.Data;

namespace Middleware_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Authenticates the user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>An IActionResult representing the result of the authentication.</returns>
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            // Implement authentication logic using middleware
            var user = UserData.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Authentication successful, return a JWT token or other authentication token
                // For demonstration purposes, returning a simple message
                return Ok("Authentication successful");
            }
            else
            {
                // Authentication failed, return 401 Unauthorized response
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
