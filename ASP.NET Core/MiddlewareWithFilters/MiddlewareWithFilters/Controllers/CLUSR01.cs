using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiddlewareWithFilters.BL.Interfaces;
using MiddlewareWithFilters.Data;
using MiddlewareWithFilters.Helpers;
using MiddlewareWithFilters.Models;
using MiddlewareWithFilters.Models.DTO;
using MiddlewareWithFilters.Models.POCO;
using ServiceStack.OrmLite;
using System.Data;

namespace MiddlewareWithFilters.Controllers
{
    /// <summary>
    /// Controller for handling user operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01 : ControllerBase
    {
        #region Private Members
        /// <summary>
        /// The user service.
        /// </summary>
        private readonly IUSR01Service _userService;

        /// <summary>
        /// The database connection factory.
        /// </summary>
        private readonly DbConnectionFactory _db;
        #endregion

        #region Public Member
        /// <summary>
        /// The response object used for returning results.
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLUSR01"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="db">The database connection factory.</param>
        public CLUSR01(IUSR01Service userService, DbConnectionFactory db)
        {
            _userService = userService;
            _db = db;
        }
        #endregion

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An IActionResult containing the list of all users.</returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An IActionResult containing the user information.</returns>
        [HttpGet("Get/{id:int}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            return Ok(_userService.Get(id));
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="objDto">The user data transfer object.</param>
        /// <returns>An IActionResult containing the response of the add operation.</returns>
        [HttpPost("Add")]
        [AllowAnonymous]
        public IActionResult Post(DTOUSR01 objDto)
        {
            response = new Response();
            _userService.Operation = enmOperation.I;
            _userService.PreSave(objDto);
            response = _userService.Validate();
            if (!response.HasError)
                response = _userService.Save();
            return Ok(response);
        }

        /// <summary>
        /// Creates the USR01 table in the database.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost("CreateTable")]
        public IActionResult Post()
        {
            using (IDbConnection db = _db.CreateConnection())
            {
                db.CreateTable<USR01>();
            }

            return Ok();
        }

        /// <summary>
        /// Generates a custom exception for testing purposes.
        /// </summary>
        /// <returns>Throws an exception.</returns>
        [HttpGet("Error")]
        public IActionResult Error()
        {
            throw new Exception("Custom Exception");
        }
    }
}
