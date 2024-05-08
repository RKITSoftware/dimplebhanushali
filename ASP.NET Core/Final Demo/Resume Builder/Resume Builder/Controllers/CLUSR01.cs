using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Helpers;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;
using Resume_Builder.Models;
using Microsoft.AspNetCore.Authorization;
using Resume_Builder.DL.Interfaces;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01 : ControllerBase
    {
        private readonly ICRUDService<USR01> _crudService;
        public Response response;

        public CLUSR01(ICRUDService<USR01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }

        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] DTOUSR01 model)
        {
            CRUDImplementation<USR01>.operation = EnumMessage.I;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
                _crudService.SendEmail(model.R01F04, "Welcome to Certificate Generator");
            }
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] DTOUSR01 model)
        {
            CRUDImplementation<USR01>.operation = EnumMessage.U;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CRUDImplementation<USR01>.operation = EnumMessage.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns> User's Information </returns>
        [HttpGet]
        [Route("info")]
        public IActionResult GetUserInfo()
        {
            return Ok(_crudService.GetUserDetails(HttpContext.GetUserIdFromClaims()));
        }
    }
}
