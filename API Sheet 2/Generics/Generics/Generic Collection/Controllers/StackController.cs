using Generic_Collection.BL;
using Generic_Collection.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Generic_Collection.Controllers
{
    /// <summary>
    /// Controller for managing a stack of Person objects through a Web API.
    /// </summary>
    [RoutePrefix("api")]
    public class StackController : ApiController
    {
        // Static stack to store Person objects
        private static PersonsBL _persons = new PersonsBL();

        /// <summary>
        /// Get all Person records from the stack.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllPersons()
        {
            return Ok(_persons.GetAllPersonDetails());
        }

        /// <summary>
        /// Get the top Person record from the stack without removing it.
        /// </summary>
        [HttpGet]
        [Route("GetPerson")]
        public IHttpActionResult GetPerson()
        {
            return Ok(_persons.GetPerson());
        }

        /// <summary>
        /// Add a new Person to the stack.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddPerson(Person objPerson)
        {
            return Ok(_persons.AddPerson(objPerson));
        }

        /// <summary>
        /// Remove the top Person record from the stack.
        /// </summary>
        [HttpDelete]
        [Route("Remove")]
        public IHttpActionResult RemovePerson()
        {
            return Ok(_persons.RemovePerson());
        }
    }
}
