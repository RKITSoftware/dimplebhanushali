using List_To_Set.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BCL_Demo.Controllers
{
    /// <summary>
    /// Controller For Base Class Library Demo
    /// </summary>
    [RoutePrefix("api/set")]
    public class SetController : ApiController
    {
        /// <summary>
        /// Instance of Referenced Library.
        /// </summary>
        private static ListToSet<dynamic> _mySet;
        
        /// <summary>
        /// Default Controller for Initialising _mySet
        /// </summary>
        static SetController()
        {
            _mySet = new ListToSet<dynamic>();
        }

        /// <summary>
        /// Get All Items
        /// </summary>
        /// <returns>Item List</returns>
        [HttpGet, Route("GetAll")]
        public IHttpActionResult GetAllItems()
        {
            // Convert _mySet to a list before returning
            var itemList = _mySet.ToList();
            return Ok(itemList);
        }

        /// <summary>
        /// Add Item to List
        /// </summary>
        /// <param name="item">Dynamic Item</param>
        /// <returns>Result</returns>
        [HttpPost]
        [Route("AddItem")]
        public HttpResponseMessage AddItem([FromBody] dynamic item)
        {
            try
            {
                string result = _mySet.Add(item);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove Item From List
        /// </summary>
        /// <param name="item">Dynamic Item</param>
        /// <returns>Result</returns>
        [HttpPost, Route("Remove")]
        public IHttpActionResult RemoveItem([FromBody] dynamic item)
        {
            string result = _mySet.Remove(item);
            return Ok(result);
        }

    }
}
