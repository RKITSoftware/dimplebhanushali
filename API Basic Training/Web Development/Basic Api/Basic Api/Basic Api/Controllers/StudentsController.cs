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
        // Gets all students.
        [HttpGet]
        [Route("api/GetAllStudents")]
        public IHttpActionResult GetAllStudents()
        {
            List<Student> students = studentBL.GetAllStudents();
            return Ok(students);
        }

        // Gets a student by ID.
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

        // Adds a new student.
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

        // Edits an existing student.
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

        // Deletes a student by ID.
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
