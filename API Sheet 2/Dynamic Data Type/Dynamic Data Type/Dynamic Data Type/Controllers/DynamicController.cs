using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dynamic_Data_Type.BL;

namespace Dynamic_Data_Type.Controllers
{
    /// <summary>
    /// Controller for handling dynamic data in an ASP.NET Web API.
    /// </summary>
    [RoutePrefix("api/Dynamic")]
    public class DynamicController : ApiController
    {
        private static DynamicBL _dynamicBL = new DynamicBL();

        /// <summary>
        /// Adds dynamic data to the list.
        /// </summary>
        /// <param name="data">Dynamic data to be added.</param>
        /// <returns>HttpResponseMessage indicating the status of the operation.</returns>
        [HttpPost, Route("AddData")]
        public HttpResponseMessage AddData([FromBody] dynamic data)
        {
            try
            {
                _dynamicBL.AddDynamicData(data);
                return Request.CreateResponse(HttpStatusCode.OK, $"Data Added => {data}");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all dynamically received data.
        /// </summary>
        /// <returns>IHttpActionResult containing the list of dynamic data.</returns>
        [HttpGet, Route("GetAllData")]
        public IHttpActionResult GetAllData()
        {
            return Ok(_dynamicBL.GetAllDynamicData());
        }

        /// <summary>
        /// Gets a specific item from the list by index.
        /// </summary>
        /// <param name="id">Index of the item to retrieve.</param>
        /// <returns>IHttpActionResult containing the dynamically received data at the specified index.</returns>
        [HttpGet, Route("GetData/{id}")]
        public IHttpActionResult Get([FromUri] int id)
        {
            var dynamicData = _dynamicBL.GetDynamicDataById(id);

            if (dynamicData != null)
            {
                return Ok(dynamicData);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a specific item in the list by index.
        /// </summary>
        /// <param name="id">Index of the item to update.</param>
        /// <param name="updatedData">Updated dynamic data.</param>
        /// <returns>HttpResponseMessage indicating the status of the operation.</returns>
        [HttpPut, Route("EditData/{id}")]
        public HttpResponseMessage EditData([FromUri] int id, [FromBody] dynamic updatedData)
        {
            try
            {
                bool success = _dynamicBL.UpdateDynamicData(id, updatedData);

                if (success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Updated data at index {id}");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Index not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a specific item from the list by index.
        /// </summary>
        /// <param name="id">Index of the item to delete.</param>
        /// <returns>HttpResponseMessage indicating the status of the operation.</returns>
        [HttpDelete, Route("DeleteData/{id}")]
        public HttpResponseMessage DeleteData([FromUri] int id)
        {
            try
            {
                bool success = _dynamicBL.DeleteDynamicDataById(id);

                if (success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Deleted data at index {id}");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Index not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes all dynamically received data.
        /// </summary>
        /// <returns>IHttpActionResult indicating the status of the operation.</returns>
        [HttpDelete, Route("DeleteAll")]
        public IHttpActionResult DeleteAll()
        {
            _dynamicBL.DeleteAllDynamicData();
            return Ok("All data deleted");
        }
    }
}
