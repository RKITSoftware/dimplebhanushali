using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dynamic_Data_Type.Controllers
{
    /// <summary>
    /// Controller for handling dynamic data in an ASP.NET Web API.
    /// </summary>
    public class DynamicController : ApiController
    {
        // List to store dynamically received data
        private static List<dynamic> _dynamicDataList = new List<dynamic>();

        /// <summary>
        /// Adds dynamic data to the list.
        /// </summary>
        /// <param name="data">Dynamic data to be added.</param>
        /// <returns>HttpResponseMessage indicating the status of the operation.</returns>
        // POST: api/dynamic
        public HttpResponseMessage Post([FromBody] dynamic data)
        {
            try
            {
                _dynamicDataList.Add(data);
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
        // GET: api/dynamic
        public IHttpActionResult Get()
        {
            return Ok(_dynamicDataList);
        }

        /// <summary>
        /// Gets a specific item from the list by index.
        /// </summary>
        /// <param name="id">Index of the item to retrieve.</param>
        /// <returns>IHttpActionResult containing the dynamically received data at the specified index.</returns>
        // GET: api/dynamic/1
        public IHttpActionResult Get(int id)
        {
            if (id >= 0 && id < _dynamicDataList.Count)
            {
                return Ok(_dynamicDataList[id]);
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
        // PUT: api/dynamic/1
        public HttpResponseMessage Put(int id, [FromBody] dynamic updatedData)
        {
            try
            {
                if (id >= 0 && id < _dynamicDataList.Count)
                {
                    _dynamicDataList[id] = updatedData;
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
        // DELETE: api/dynamic/1
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id >= 0 && id < _dynamicDataList.Count)
                {
                    _dynamicDataList.RemoveAt(id);
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
        // DELETE: api/dynamic
        public IHttpActionResult DeleteAll()
        {
            _dynamicDataList.Clear();
            return Ok("All data deleted");
        }
    }
}
