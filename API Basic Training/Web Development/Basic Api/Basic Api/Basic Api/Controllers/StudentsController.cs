using Basic_Api.Models;
using System.Linq;
using System.Web.Http;

namespace Basic_Api.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        [Route("api/GetAllStudents")]
        public IHttpActionResult GetAllStudents()
        { 
            return Ok(Student.lstStudents);
        }

        [HttpGet]
        [Route("api/GetStudentById/{id}")]
        public IHttpActionResult GetStudentById([FromUri] int id)
        {
            Student objStudent = Student.lstStudents.FirstOrDefault(stu => stu.Id == id);
            return Ok(objStudent);
        }

        [HttpPost]
        [Route("api/AddStudent")]
        public IHttpActionResult AddStudent(Student objStudent)
        {
            if (objStudent == null) 
            {
                return BadRequest("Invalid Input Data !!!");
            }
            Student objStu = new Student();
            objStu.Id = Student.lstStudents.Count + 1;
            objStu.Name = objStudent.Name;
            objStu.Age = objStudent.Age;
            objStu.Course= objStudent.Course;
            Student.lstStudents.Add(objStu);
            return Ok("Student Added Successfully");
        }

        [HttpPut]
        [Route("api/EditStudent/{id}")]
        public IHttpActionResult EditStudent([FromUri] int id,Student objStudent)
        {
            if (objStudent == null) 
            {
                return BadRequest("Invalid Input Data !!!");
            }
            Student objEditableStudent = Student.lstStudents.FirstOrDefault(stu => stu.Id == id);
            objEditableStudent.Name = objStudent.Name;
            objEditableStudent.Age = objStudent.Age;
            objEditableStudent.Course = objStudent.Course;
            return Ok($"Student with {id} Updated SuccessFully");
        }

        [HttpDelete]
        [Route("api/DeleteStudent/{id}")]
        public IHttpActionResult DeleteStudent([FromUri] int id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Id");
            }
            Student.lstStudents.Remove(Student.lstStudents.FirstOrDefault(stu => stu.Id == id));
            return Ok($"Student with {id} Deleted SuccessFully");
        }


    }
}