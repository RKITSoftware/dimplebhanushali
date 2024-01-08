using Basic_Authentication.Data;
using Basic_Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Basic_Authentication.Controllers
{
    public class EmployeeController : ApiController
    {
        AppDbContext db = new AppDbContext();

        [Authorize(Roles = ("user"))]
        public HttpResponseMessage GetEmployeeById(int id)
        {
            var user = db.employees.FirstOrDefault(e => e.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK,user);
        }

        [Authorize(Roles = ("admin"))]
        [Route("api/Employee/GetEmployees")]
        public HttpResponseMessage GetEmployees()
        {
            var user = db.employees.ToList();
            return Request.CreateResponse(HttpStatusCode.OK,user);
        }

    }
}
