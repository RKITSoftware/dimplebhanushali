using Filters_Web_API.BL;
using Filters_Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filters_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUserController : ControllerBase
    {
        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLUser objBLUser;

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLUserController()
        {
            objBLUser = new BLUser();
        }

        /// <summary>
        /// Handles request for get user
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(BLUser.lstUser);
        }

        /// <summary>
        /// Handles request for add user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Appropriate message</returns>
        [HttpPost("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser(User objUSR01)
        {
            if (objBLUser.Validation(objUSR01))
            {
                return Ok(objBLUser.AddUser(objUSR01));
            }
            else
            {
                return BadRequest("Invalid data!");
            }
        }
    }
}
