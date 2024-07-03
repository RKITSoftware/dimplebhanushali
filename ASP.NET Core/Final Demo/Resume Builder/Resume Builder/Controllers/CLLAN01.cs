﻿using Microsoft.AspNetCore.Mvc;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.Helpers;
using Resume_Builder.Models;
using Resume_Builder.Models.DTO;
using Resume_Builder.Models.POCO;

namespace Resume_Builder.Controllers
{
    /// <summary>
    /// Controller responsible for CRUD operations related to language details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLLAN01 : ControllerBase
    {
        #region Private Member
        /// <summary>
        /// instance of ICRUDService<LAN01>
        /// </summary>
        private readonly ICRUDService<LAN01> _crudService;
        #endregion

        #region Public Member
        /// <summary>
        /// instance of Response
        /// </summary>
        public Response response;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CLLAN01"/> class.
        /// </summary>
        /// <param name="crudService">The CRUD service for language details.</param>
        public CLLAN01(ICRUDService<LAN01> crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
        }
        #endregion

        /// <summary>
        /// Retrieves all language details.
        /// </summary>
        /// <returns>An HTTP response containing the list of language details.</returns>
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            response = new Response();
            response = _crudService.GetData();
            return Ok(response);
        }

        /// <summary>
        /// Retrieves language details associated with the current user.
        /// </summary>
        /// <returns>An HTTP response containing the list of language details.</returns>
        [HttpGet, Route("GetById")]
        public IActionResult Get()
        {
            response = new Response();
            response = _crudService.GetById(HttpContext.GetUserIdFromClaims());
            return Ok(response);
        }

        /// <summary>
        /// Adds a new language detail.
        /// </summary>
        /// <param name="model">The language detail data to add.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DTOLAN01 model)
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
        /// Updates an existing language detail.
        /// </summary>
        /// <param name="model">The language detail data to update.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [HttpPut]
        public IActionResult Put([FromBody] DTOLAN01 model)
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
        /// Deletes a language detail.
        /// </summary>
        /// <param name="id">The ID of the language detail to delete.</param>
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
