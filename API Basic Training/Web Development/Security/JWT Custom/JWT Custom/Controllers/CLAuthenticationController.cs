using JWT_Custom.BL;
using JWT_Custom.Models;
using System.Web.Http;

namespace JWT_Custom.Controllers
{
    public class CLAuthenticationController : ApiController
    {
        /// <summary>
        /// Generates a JWT token for the given username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>HTTP response containing the generated JWT token in the data field.</returns>
        [HttpPost]
        [Route("token")]
        public IHttpActionResult GenerateJwtToken(string username, string password)
        {
            // Retrieve user information based on the provided username and password
            User objUser = BLUser.GetUser(username, password);

            // If the user is not found, return a 404 Not Found response
            if (objUser == null)
                return NotFound();

            // Generate a JWT token using the user information and return it in the response
            return Ok(new { data = BLJWT.GenerateToken(objUser) });
        }
    }
}
