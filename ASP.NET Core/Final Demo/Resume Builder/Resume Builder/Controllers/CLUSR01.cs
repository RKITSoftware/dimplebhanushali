using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01 : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// Instance of ICRUDService.
        /// </summary>
        private readonly ICRUDService<USR01> _crudService;
        #endregion

        #region Public Member
        /// <summary>
        /// Instance of Response.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLUSR01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for managing user data.</param>
        public CLUSR01(ICRUDService<USR01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }
        #endregion

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An HTTP response containing the list of users.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves user information by ID.
        /// </summary>
        /// <returns>An HTTP response containing the user's information.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The user data to register.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] DTOUSR01 model)
        {
            _crudService.operation = enmOperation.I;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
                _crudService.SendEmail(model.R01F04, "Welcome to Certificate Generator");
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="model">The updated user data.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOUSR01 model)
        {
            _crudService.operation = enmOperation.U;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _crudService.operation = enmOperation.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves details of the authenticated user.
        /// </summary>
        /// <returns>An HTTP response containing the authenticated user's details.</returns>
        [HttpGet]
        [Route("info")]
        public IActionResult GetUserInfo()
        {
            return Ok(_crudService.GetUserDetails(HttpContext.GetUserIdFromClaims()));
        }
    }
}
