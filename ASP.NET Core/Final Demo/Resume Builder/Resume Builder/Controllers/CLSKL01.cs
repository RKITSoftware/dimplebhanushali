using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to skills.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLSKL01 : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// instance of ICRUDService<SKL01>
        /// </summary>
        private readonly ICRUDService<SKL01> _crudService;
        #endregion

        #region Public Member
        /// <summary>
        /// Instance of Response
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLSKL01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for skills.</param>
        public CLSKL01(ICRUDService<SKL01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }
        #endregion

        /// <summary>
        /// Retrieves all skills.
        /// </summary>
        /// <returns>An HTTP response containing the list of skills.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.GetData();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves skills associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of skills.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.GetById(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new skill.
        /// </summary>
        /// <param name="model">The skill data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOSKL01 model)
        {
            _crudService.operation = enmOperation.I;
            response = _crudService.PreValidation(model.UserId);
            if (!response.HasError)
            {
                _crudService.PreSave(model);
                response = _crudService.Validate();
                if (!response.HasError)
                {
                    response = _crudService.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing skill.
        /// </summary>
        /// <param name="model">The skill data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOSKL01 model)
        {
            _crudService.operation = enmOperation.U;
            response = _crudService.PreValidation(model.UserId);
            if (!response.HasError)
            {
                _crudService.PreSave(model);
                response = _crudService.Validate();
                if (!response.HasError)
                {
                    response = _crudService.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a skill.
        /// </summary>
        /// <param name="id">The ID of the skill to delete.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _crudService.operation = enmOperation.D;
            response = _crudService.ValidateOnDelete(id);
            if (!response.HasError)
            {
                response = _crudService.Delete(id);
            }

            return Ok(response);
        }
    }
}
