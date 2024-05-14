using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Helpers;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to certifications.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLCER01 : ControllerBase
    {
        private readonly ICRUDService<CER01> _crudService;
        public Response response;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLCER01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for certifications.</param>
        public CLCER01(ICRUDService<CER01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }

        /// <summary>
        /// Retrieves all certifications.
        /// </summary>
        /// <returns>An HTTP response containing the list of certifications.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves certifications associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of certifications.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new certification.
        /// </summary>
        /// <param name="model">The certification data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOCER01 model)
        {
            CRUDImplementation<CER01>.operation = EnumMessage.I;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing certification.
        /// </summary>
        /// <param name="model">The certification data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOCER01 model)
        {
            CRUDImplementation<CER01>.operation = EnumMessage.U;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a certification.
        /// </summary>
        /// <param name="id">The ID of the certification to delete.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CRUDImplementation<CER01>.operation = EnumMessage.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }
    }
}
