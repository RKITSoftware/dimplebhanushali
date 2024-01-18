using Generics.Models;
using Generics.Person_Repository;
using System.Web.Http;

namespace Generics.Controllers
{
    [RoutePrefix("api")]
    public class PersonController : ApiController
    {
        private PersonRepo _persons = new PersonRepo();

        /// <summary>
        /// Gets all persons.
        /// </summary>
        [HttpGet]
        [Route("AllPersons")]
        public IHttpActionResult GetAllPersons()
        {
            return Ok(_persons.GetAll());
        }

        /// <summary>
        /// Gets a person by ID.
        /// </summary>
        [HttpGet]
        [Route("Person/{id}")]
        public IHttpActionResult GetPerson(int id)
        {
            return Ok(_persons.GetById(id));
        }

        /// <summary>
        /// Adds a new person.
        /// </summary>
        [HttpPost]
        [Route("AddPerson")]
        public IHttpActionResult AddPerson(Person objPerson)
        {
            return Ok(_persons.Add(objPerson));
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        [HttpPut]
        [Route("Update/{id}")]
        public IHttpActionResult EditPerson(int id, Person objPerson)
        {
            return Ok(_persons.Update(id, objPerson));
        }

        /// <summary>
        /// Deletes a person by ID.
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeletePerson(int id)
        {
            return Ok(_persons.Delete(id));
        }
    }
}
