using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to education details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLEDU01 : ControllerBase
    {
        #region private member
        /// <summary>
        /// instance of ICRUDService<EDU01>
        /// </summary>
        private readonly ICRUDService<EDU01> _crudService;

        #endregion

        #region public member
        /// <summary>
        /// instance of Response
        /// </summary>
        public Response response;

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLEDU01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for education details.</param>
        public CLEDU01(ICRUDService<EDU01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }

        #endregion

        #region Public Method
        /// <summary>
        /// Retrieves all education details.
        /// </summary>
        /// <returns>An HTTP response containing the list of education details.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.Get();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves education details associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of education details.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.Get(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new education detail.
        /// </summary>
        /// <param name="model">The education detail data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOEDU01 model)
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
        /// Updates an existing education detail.
        /// </summary>
        /// <param name="model">The education detail data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOEDU01 model)
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
        /// Deletes an education detail.
        /// </summary>
        /// <param name="id">The ID of the education detail to delete.</param>
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
