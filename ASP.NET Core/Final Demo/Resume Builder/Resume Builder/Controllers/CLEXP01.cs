using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to work experience details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLEXP01 : ControllerBase
    {
        #region Private member
        /// <summary>
        /// instance of ICRUDService<EXP01>
        /// </summary>
        private readonly ICRUDService<EXP01> _crudService;

        #endregion

        #region Public Member

        /// <summary>
        /// instance of Response
        /// </summary>
        public Response response;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CLEXP01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for work experience details.</param>
        public CLEXP01(ICRUDService<EXP01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }

        #endregion

        #region Public method
        /// <summary>
        /// Retrieves all work experience details.
        /// </summary>
        /// <returns>An HTTP response containing the list of work experience details.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves work experience details associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of work experience details.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new work experience detail.
        /// </summary>
        /// <param name="model">The work experience detail data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOEXP01 model)
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
        /// Updates an existing work experience detail.
        /// </summary>
        /// <param name="model">The work experience detail data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOEXP01 model)
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
        /// Deletes a work experience detail.
        /// </summary>
        /// <param name="id">The ID of the work experience detail to delete.</param>
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

        #endregion
    }
}
