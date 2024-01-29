using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Token_Auth.Models;

namespace Token_Auth.Controllers
{
    public class EmployeeController : ApiController
    {

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("api/emp/{id}")]
        public HttpResponseMessage GetEmployeeById([FromUri] int id) 
        {
            Employee objEmp = Employee.lstEmployees.FirstOrDefault(x => x.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK,objEmp);
        }

        [Authorize(Roles = "admin,superadmin")]
        [Route("api/some")]
        [HttpGet]
        public HttpResponseMessage GetSomeEmployee() 
        {
            Employee objEmp = Employee.lstEmployees.FirstOrDefault(x => x.Id <= 2);
            return Request.CreateResponse(HttpStatusCode.OK,objEmp);
        }

        [Authorize(Roles = "superadmin")]
        [Route("api/all")]
        [HttpGet]
        public HttpResponseMessage GetEmployees() 
        {
            List<Employee> lstEmp = Employee.lstEmployees.ToList();
            return Request.CreateResponse(HttpStatusCode.OK,lstEmp);
        }
    }
}
