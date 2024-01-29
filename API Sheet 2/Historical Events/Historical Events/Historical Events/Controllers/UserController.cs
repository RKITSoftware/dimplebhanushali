using Historical_Events.BL;
using Historical_Events.Models;
using System;
using System.Configuration;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly BLUser _userManager;

        public UserController()
        {
            _userManager = new BLUser(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        }

        [HttpPost, Route("Register")]
        public IHttpActionResult RegisterUser(User objUser)
        {
            try
            {
                string result = _userManager.RegisterUser(objUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("Login")]
        public IHttpActionResult LoginUser(string userName, string password)
        {
            try
            {
                bool isAuthenticated = _userManager.LoginUser(userName, password);

                if (isAuthenticated)
                {
                    return Ok("Login successful");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = BLUser.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var user = _userManager.GetUserById(id);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpDelete, Route("Delete/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var result = _userManager.DeleteUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }
    }
}
