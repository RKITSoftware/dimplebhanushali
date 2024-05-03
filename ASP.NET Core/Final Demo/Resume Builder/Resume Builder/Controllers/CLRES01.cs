using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLRES01 : ControllerBase
    {
        private readonly ICRUDService<RES01> _crudService;
        public Response response;

        public CLRES01(ICRUDService<RES01> crudService)
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
        public IActionResult Post([FromBody] DTORES01 model)
        {
            CRUDImplementation<RES01>.operation = EnumMessage.I;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] DTORES01 model)
        {
            CRUDImplementation<RES01>.operation = EnumMessage.U;
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
            CRUDImplementation<RES01>.operation = EnumMessage.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }
    }
}
