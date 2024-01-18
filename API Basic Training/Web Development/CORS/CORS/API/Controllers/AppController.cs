using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AppController : ApiController
    {
        private static List<Students> lstStudents = new List<Students>
        {
            new Students { Id = 1, Name = "Dimple", Course = "MCA", Sem = 4 },
            new Students { Id = 2, Name = "Ankit", Course = "MSC", Sem = 3 },
            new Students { Id = 3, Name = "Pankaj", Course = "BCA", Sem = 2 },
            new Students { Id = 4, Name = "Krishna", Course = "EC", Sem = 1 },
            new Students { Id = 5, Name = "Abc", Course = "MCA", Sem = 4 },
        };

        [HttpPost]
        public IHttpActionResult AddStudents(Students stu)
        {
            Console.WriteLine("Received POST request to AddStudents");

            if (stu != null)
            {
                Students student = new Students();
                student.Id = stu.Id;
                student.Name = stu.Name;
                student.Course = stu.Course;
                student.Sem = stu.Sem;

                lstStudents.Add(student);

                string successMessage = "Student added successfully!";
                return Ok(new { Message = successMessage, Student = stu });
            }

            Console.WriteLine("Invalid Student Data");
            return BadRequest("Invalid Student Data");
        }

        [HttpGet]
        [Route("api/GetStudents")]
        public IHttpActionResult GetStudents()
        {
            return Ok(lstStudents);
        }

        [HttpPut]
        public IHttpActionResult UpdateStudent(int id, Students stu)
        {
            if (stu != null)
            {
                Students objStu = lstStudents.FirstOrDefault(std => std.Id == id);

                if (objStu != null)
                {
                    objStu.Name = stu.Name;
                    objStu.Sem = stu.Sem;
                    objStu.Course = stu.Course;

                    return Ok(objStu);
                }
                else
                {
                    return NotFound();
                }
            }

            return BadRequest("Invalid Student Data");
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            try
            {
                Students objStu = lstStudents.FirstOrDefault(stu => stu.Id == id);

                if (objStu != null)
                {
                    lstStudents.Remove(objStu);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return BadRequest();
            }
        }


    }
}
