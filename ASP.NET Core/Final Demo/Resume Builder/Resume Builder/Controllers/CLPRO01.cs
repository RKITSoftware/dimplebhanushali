using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to project details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLPRO01 : ControllerBase
    {
        /// <summary>
        /// instance of ICRUDService<PRO01>
        /// </summary>
        private readonly ICRUDService<PRO01> _crudService;

        /// <summary>
        /// instance of Response
        /// </summary>
        public Response response;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLPRO01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for project details.</param>
        public CLPRO01(ICRUDService<PRO01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }

        /// <summary>
        /// Retrieves all project details.
        /// </summary>
        /// <returns>An HTTP response containing the list of project details.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves project details associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of project details.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new project detail.
        /// </summary>
        /// <param name="model">The project detail data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOPRO01 model)
        {
            BLCRUDImplementation<PRO01>.operation = enmMessage.I;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing project detail.
        /// </summary>
        /// <param name="model">The project detail data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOPRO01 model)
        {
            BLCRUDImplementation<PRO01>.operation = enmMessage.U;
            _crudService.PreSave(model);
            response = _crudService.Validate();
            if (!response.HasError)
            {
                response = _crudService.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a project detail.
        /// </summary>
        /// <param name="id">The ID of the project detail to delete.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BLCRUDImplementation<PRO01>.operation = enmMessage.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }
    }
}
