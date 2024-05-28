using Historical_Events.BL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/User")]
    public class CLUserController : ApiController
    {
        #region Private Member
        /// <summary>
        /// Instance of Business Logic of User Class.
        /// </summary>
        private readonly BLUser _userManager;
        #endregion

        #region Public Member
        /// <summary>
        /// Instance of Response Class.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for initialising new instance of User BL Class.
        /// </summary>
        public CLUserController()
        {
            _userManager = new BLUser(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        }
        #endregion

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

        #region Private Method
        /// <summary>
        /// Gets Current Logged in User Id from Claims.
        /// </summary>
        /// <returns>Current User id Logged in.</returns>
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
        #endregion
    }
}
