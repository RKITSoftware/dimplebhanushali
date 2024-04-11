using Basic_Api.BL;
using Basic_Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Basic_Api.Controllers
{
    /// <summary>
    /// API Controller for managing student data.
    /// </summary>
    public class StudentsController : ApiController
    {
        StudentBL studentBL = new StudentBL();

        /// <summary>
        /// Gets all students.
        /// </summary>
        /// <returns>List of all students.</returns>
        [HttpGet]
        [Route("api/GetAllStudents")]
        public IHttpActionResult GetAllStudents()
        {
            List<Student> students = studentBL.GetAllStudents();
            return Ok(students);
        }

        /// <summary>
        /// Gets a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student with the specified ID.</returns>
        [HttpGet]
        [Route("api/GetStudentById/{id}")]
        public IHttpActionResult GetStudentById(int id)
        {
            Student objStudent = studentBL.GetStudentById(id);
            if (objStudent != null)
            {
                return Ok(objStudent);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStudent">The student to be added.</param>
        /// <returns>Result of the add operation.</returns>
        [HttpPost]
        [Route("api/AddStudent")]
        public IHttpActionResult AddStudent(Student objStudent)
        {
            string result = studentBL.AddStudent(objStudent);
            if (result.Contains("Successfully"))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Edits an existing student.
        /// </summary>
        /// <param name="id">The ID of the student to be edited.</param>
        /// <param name="objStudent">The updated student information.</param>
        /// <returns>Result of the edit operation.</returns>
        [HttpPut]
        [Route("api/EditStudent/{id}")]
        public IHttpActionResult EditStudent(int id, Student objStudent)
        {
            string result = studentBL.EditStudent(id, objStudent);
            if (result.Contains("Successfully"))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete]
        [Route("api/DeleteStudent/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            string result = studentBL.DeleteStudent(id);
            if (result.Contains("Successfully"))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
