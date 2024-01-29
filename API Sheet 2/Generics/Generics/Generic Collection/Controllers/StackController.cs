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
        private static Stack<Person> _persons = new Stack<Person> { };

        /// <summary>
        /// Constructor to initialize the stack with 10 Person records.
        /// </summary>
        public StackController()
        {
            // Initialize the stack before using it
            _persons = new Stack<Person>();

            // Push persons onto the stack
            for (int i = 1; i < 11; i++)
            {
                _persons.Push(new Person
                {
                    Id = i,
                    Name = "Person " + i,
                    Age = 23,
                });
            }
        }

        /// <summary>
        /// Get all Person records from the stack.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(_persons);
        }

        /// <summary>
        /// Get the top Person record from the stack without removing it.
        /// </summary>
        [HttpGet]
        [Route("GetStudent")]
        public IHttpActionResult GetStudent()
        {
            return Ok(_persons.Peek());
        }

        /// <summary>
        /// Add a new Person to the stack.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddStudent(Person person)
        {
            _persons.Push(person);
            return Ok(_persons);
        }

        /// <summary>
        /// Remove the top Person record from the stack.
        /// </summary>
        [HttpDelete]
        [Route("Remove")]
        public IHttpActionResult RemoveStudent()
        {
            _persons.Pop();
            return Ok(_persons);
        }
    }
}
