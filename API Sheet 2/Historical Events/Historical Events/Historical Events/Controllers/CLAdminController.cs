using Historical_Events.Basic_Authorisation;
using Historical_Events.BL;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    /// <summary>
    /// Controller for managing Admin-related operations.
    /// </summary>
    [RoutePrefix("api/User")]
    [BasicAuthenticationFilter]
    [BasicAuthorisation(Roles = "A")]
    public class CLAdminController : ApiController
    {
        /// <summary>
        ///  Instance of Business Logic for Historical Events manager.
        /// </summary>
        private readonly BLHistory _blHistory;

        /// <summary>
        /// Instance of Business Logic of User Class.
        /// </summary>
        private readonly BLUser _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLHistoryController"/> class.
        /// </summary>
        public Response response;

        /// <summary>
        /// Constructor For Initialising BlHistory and BlUser Instances.
        /// </summary>
        public CLAdminController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            _blHistory = new BLHistory(connectionString);
            _userManager = new BLUser(connectionString);

        }

        /// <summary>
        /// Creates a new historical event.
        /// </summary>
        [HttpPost, Route("Create")]
        public IHttpActionResult CreateHistoricalEvent(DTOHstEvt01 objDTOHstEvt01)
        {
            _blHistory.enmOperation = Helpers.enmOperation.I;

            _blHistory.Presave(objDTOHstEvt01);

            response = _blHistory.Validate();
            if (!response.isError)
            {
                response = _blHistory.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Edits an existing historical event.
        /// </summary>
        [HttpPut, Route("Edit/{id}")]
        public IHttpActionResult EditHistoricalEvent(DTOHstEvt01 objDTOHstEvt01)
        {
            _blHistory.enmOperation = Helpers.enmOperation.U;

            _blHistory.Presave(objDTOHstEvt01);

            response = _blHistory.Validate();
            if (response.isError)
            {
                return Ok(response);
            }

            response = _blHistory.Save();
            return Ok(response);
        }

        /// <summary>
        /// Deletes a historical event.
        /// </summary>
        [HttpDelete, Route("Delete/{id}")]
        public IHttpActionResult DeleteHistoricalEvent(int id)
        {
            response = _blHistory.ValidateOnDelete(id);
            if (response.isError)
            {
                return Ok(response);
            }

            response = _blHistory.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all users. (Requires admin or superadmin role)
        /// </summary>
        [HttpGet, Route("GetAll")]
        [BasicAuthorisation(Roles = "Admin, SuperAdmin")]
        public IHttpActionResult GetAllUsers()
        {
            response = _userManager.GetAllUsers();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            response = _userManager.GetUserById(id);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet, Route("GetDetails")]
        public IHttpActionResult GetDetails()
        {
            int id = GetCurrentUser();
            response = _userManager.GetUserById(id);
            return Ok(response);
        }

        /// <summary>
        /// Method Gets Current USer Id from Claims
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
