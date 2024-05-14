using Historical_Events.BL;
using Historical_Events.Helpers;
using Historical_Events.Models;
using Historical_Events.Models.DTO;
using System.Configuration;
using System.Web.Http;

namespace Historical_Events.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly BLUser _userManager;
        
        public Response response;

        public UserController()
        {
            _userManager = new BLUser(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        }

        [HttpPost, Route("Register")]
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

        [HttpPost,Route("CreateTables")]
        public IHttpActionResult CreateTable()
        {
            _userManager.CreateTables();
            return Ok();
        }

        [HttpPost, Route("Login")]
        public IHttpActionResult LoginUser(string userName, string password)
        {
            response = _userManager.LoginUser(userName, password);
            return Ok(response);
        }

        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAllUsers()
        {
            response = _userManager.GetAllUsers();
            return Ok(response);
        }

        [HttpGet, Route("GetById/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            response = _userManager.GetUserById(id);
            return Ok(response);
        }

        [HttpDelete, Route("Delete/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            _userManager.operation = enmOperation.D;

            response = _userManager.ValidateOnDelete(id);

            if (!response.isError)
            {
                response = _userManager.DeleteUser(id);
            }

            return Ok(response);
        }
    }
}