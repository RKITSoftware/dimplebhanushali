using Certificate_Generator.BL;
using Certificate_Generator.Helpers;
using Certificate_Generator.Models;
using Certificate_Generator.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Certificate_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01Controller : ControllerBase
    {
        private readonly BLUSR01Handler _userHandler;

        public Response response;

        public CLUSR01Controller(BLUSR01Handler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost,Route("CreateTables")]
        public IActionResult CreateTables()
        {
            _userHandler.CreateTables();
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] DTOUSR01 dtoUser)
        {
            response = new Response();
            _userHandler.operation = EnumMessage.I;
            _userHandler.PreSave(dtoUser);

            response = _userHandler.Validate();
            if (!response.HasError)
            {
                response = _userHandler.Save();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _userHandler.GetAllUSR01Async();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            Response response =  _userHandler.GetUSR01ByIdAsync(id);
            if (response.HasError)
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            //return Ok("User Fetched");
            return Ok(response);
        }


        [HttpPut]
        public IActionResult UpdateUser([FromBody] DTOUSR01 dtoUser)
        {
            response = new Response();
            _userHandler.operation = EnumMessage.U;
            _userHandler.PreSave(dtoUser);

            response = _userHandler.Validate();
            if (!response.HasError)
            {
                response = _userHandler.Save();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
         {
            response = new Response();
            _userHandler.operation = EnumMessage.D;
            response = _userHandler.ValidateOnDelete(id);
            if(!response.HasError)
            {
                response = _userHandler.DeleteUSR01(id);
            }

            return Ok(response);
        }
    }
}
