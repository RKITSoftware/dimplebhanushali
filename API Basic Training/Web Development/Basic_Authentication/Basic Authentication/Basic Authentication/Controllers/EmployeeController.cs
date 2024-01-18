using Basic_Authentication.Authorisation_Provider;
using Basic_Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Basic_Authentication.Controllers
{
    public class EmployeeController : ApiController
    {
        public List<Employee> EmpList = new List<Employee>
        {
            new Employee {Id = 1 , FirstName ="Dimple" , LastName="Mithiya", City="Rajkot"},
            new Employee {Id = 2 , FirstName ="Ankit" , LastName="Katarmal", City="London"},
            new Employee {Id = 3 , FirstName ="Ram" , LastName="Krishna", City="Ahmedabad"}
        };

        [HttpGet]
        [Authorisation(Roles ="admin")]
        [Route("api/Get")]
        public IHttpActionResult Get()
        {
            return Ok(EmpList);
        }

        [HttpGet]
        [Authorisation(Roles =("employee"))]
        [Route("api/GetbyId/{id}")]
        public IHttpActionResult GetById(int id)
        {
            Employee employee = EmpList.Find(e => e.Id == id);
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(employee);
        }

    }
}
