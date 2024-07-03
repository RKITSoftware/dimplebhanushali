using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for managing database tables.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLTables : ControllerBase
    {
        #region Public Member
        /// <summary>
        /// instance of Response
        /// </summary>
        public Response response;
        #endregion

        #region Private Member
        /// <summary>
        /// instance of BLTable
        /// </summary>
        private BLTables _blTables;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLTables"/> class.
        /// </summary>
        /// <param name="bLTables">The business logic layer for table management.</param>
        public CLTables(BLTables bLTables)
        {
            _blTables = bLTables;
        }
        #endregion

        /// <summary>
        /// Creates tables in the database.
        /// </summary>
        /// <returns>An HTTP response indicating success.</returns>
        [HttpPost]
        public IActionResult CreateTables()
        {
            _blTables.CreateTables();
            return Ok("Tables Created Successfully");
        }

        /// <summary>
        /// Drops existing tables and creates new ones in the database.
        /// </summary>
        /// <returns>An HTTP response indicating success.</returns>
        [HttpPost, Route("DropNCreateTables")]
        public IActionResult DropAndCreateTables()
        {
            _blTables.DropNCreateTables();
            return Ok("Tables Created Successfully");
        }
    }
}
