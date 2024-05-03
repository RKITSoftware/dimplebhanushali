using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLTables : ControllerBase
    {
        public Response response;
        private BLTables _blTables;

        public CLTables(BLTables bLTables)
        {
            _blTables = bLTables;
        }

        [HttpPost]
        public IActionResult CreateTables()
        {
            _blTables.CreateTables();
            return Ok("Tables Created Successfully");
        }
 
        [HttpPost,Route("DronNCreateTables")]
        public IActionResult DropAndCreateTables()
        {
            _blTables.DropNCreateTables();
            return Ok("Tables Created Successfully");
        }
    }
}
