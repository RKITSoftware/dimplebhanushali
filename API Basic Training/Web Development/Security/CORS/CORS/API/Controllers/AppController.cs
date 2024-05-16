using API.BL;
using API.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    /// <summary>
    /// API Controller for managing student data.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AppController : ApiController
    {
        private static StudentBL _students;

        static AppController()
        {
            _students = new StudentBL();
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStu">The student to be added.</param>
        /// <returns>Result of the add operation.</returns>
        [HttpPost]
        public IHttpActionResult AddStudents(Students objStu)
        {
            if (objStu != null)
            {
                _students.AddStudent(objStu);
                string successMessage = "Student added successfully!";
                return Ok(new { Message = successMessage, Student = objStu });
            }

            return BadRequest("Invalid Student Data");
        }

        /// <summary>
        /// Gets a list of all students.
        /// </summary>
        /// <returns>List of students.</returns>
        [HttpGet]
        [Route("api/GetStudents")]
        public IHttpActionResult GetStudents()
        {
            return Ok(_students.GetAllStudents());
        }

        /// <summary>
        /// Updates details of an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to be updated.</param>
        /// <param name="objStu">The updated student information.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateStudent(int id, Students objStu)
        {
            if (objStu != null)
            {
                return Ok(_students.EditStudentDetails(id, objStu));
            }

            return BadRequest("Invalid Student Data");
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            if (id == null)
            {
                return BadRequest("Id Can Not Be Null");
            }

            return Ok(_students.DeleteStudent(id));
        }
    }
}
