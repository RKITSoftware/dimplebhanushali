using Historical_Events.Basic_Authorisation;
using Historical_Events.BL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/User")]
    [BasicAuthenticationFilter]

    public class CLUserController : ApiController
    {
        /// <summary>
        /// Instance of Business Logic of User Class.
        /// </summary>
        private readonly BLUser _userManager;
        
        /// <summary>
        /// Instance of Response Class.
        /// </summary>
        public Response response;

        /// <summary>
        /// Constructor for initialising new instance of User BL Class.
        /// </summary>
        public CLUserController()
        {
            _userManager = new BLUser(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost, Route("Register")]
        [AllowAnonymous]
        public IHttpActionResult RegisterUser(DTOUSR01 objUsr)
        {
            _userManager.operation = enmOperation.I;

            _userManager.PreSave(objUsr);
            
            response = _userManager.Validate();
            if (!response.isError)
            {
                response = _userManager.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates database tables.
        /// </summary>
        [HttpPost,Route("CreateTables")]
        [BasicAuthorisation(Roles = "A")]
        //[AllowAnonymous]
        public IHttpActionResult CreateTable()
        {
            _userManager.CreateTables();
            return Ok();
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public IHttpActionResult LoginUser(string userName, string password)
        {
            response = _userManager.LoginUser(userName, password);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all users. (Requires admin or superadmin role)
        /// </summary>
        [HttpGet, Route("GetAll")]
        [BasicAuthorisation(Roles = "A")]
        public IHttpActionResult GetAllUsers()
        {
            response = _userManager.GetAllUsers();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet, Route("GetById")]
        public IHttpActionResult GetUserById()
        {
            int id = GetCurrentUser();
            response = _userManager.GetUserById(id);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete, Route("Delete")]
        public IHttpActionResult DeleteUser()
        {
            _userManager.operation = enmOperation.D;

            int id = GetCurrentUser() ;

            response = _userManager.ValidateOnDelete(id);

            if (!response.isError)
            {
                response = _userManager.DeleteUser(id);
            }

            return Ok(response);
        }

        /// <summary>
        /// Gets Current Logged in User Id from Claims.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return Convert.ToInt32(userId);
            }
            return 0;
        }
    }
}
