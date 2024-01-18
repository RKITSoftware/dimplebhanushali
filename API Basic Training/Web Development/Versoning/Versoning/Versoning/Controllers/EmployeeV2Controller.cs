using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Versoning.Models;

namespace Versoning.Controllers
{
    public class EmployeeV2Controller : ApiController
    {
        List<Employee> lstEmp = new List<Employee>()
        {
            new Employee() { Id = 1, Name= "Dimple",Age = 23 },
            new Employee() {Id = 2, Name = "Pankaj", Age = 28},
            new Employee() {Id = 3, Name = "Abc", Age = 22},
            new Employee() {Id = 4, Name = "Xyz", Age = 21},
        };

        [Route("api/v2/Employee/Get")]
        public Employee Get()
        {
            List<Employee> lstEmp2 = lstEmp.Where(emp => emp.Id >= 2).ToList();
            var objEmp = lstEmp.FirstOrDefault(s => s.Id >= 2);
            return objEmp;
        }
    }
}
