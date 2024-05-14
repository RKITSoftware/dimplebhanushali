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
        public Response response;
        private BLTables _blTables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLTables"/> class.
        /// </summary>
        /// <param name="bLTables">The business logic layer for table management.</param>
        public CLTables(BLTables bLTables)
        {
            _blTables = bLTables;
        }

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
        [HttpPost, Route("DronNCreateTables")]
        public IActionResult DropAndCreateTables()
        {
            _blTables.DropNCreateTables();
            return Ok("Tables Created Successfully");
        }
    }
}
