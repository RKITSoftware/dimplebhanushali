using Lambda_Expression.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Lambda_Expression.Controllers
{
    [RoutePrefix("api")]
    public class EmployeesController : ApiController
    {
        private List<Employee> _employees = Employee.lstEmployees;

        /// <summary>
        /// Gets all employees.
        /// </summary>
        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        [HttpGet]
        [Route("Employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee objEmp = _employees.FirstOrDefault(emp => emp.Id == id);

            if (objEmp == null)
                return NotFound(); // Return 404 if employee not found

            return Ok(objEmp);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        [HttpPost]
        [Route("AddEmployee")]
        public IHttpActionResult AddEmployee(Employee objEmp)
        {
            objEmp.Id = _employees.Count + 1;
            _employees.Add(objEmp);

            return Ok(objEmp);
        }

        /// <summary>
        /// Edits an existing employee.
        /// </summary>
        [HttpPut]
        [Route("EditEmployee/{id}")]
        public IHttpActionResult EditEmployee(int id, Employee objEmp)
        {
            Employee objExistingEmp = _employees.FirstOrDefault(emp => emp.Id == id);

            if (objExistingEmp == null)
                return NotFound(); // Return 404 if employee not found

            objExistingEmp.Name = objEmp.Name;
            objExistingEmp.Salary = objEmp.Salary;
            objExistingEmp.Department = objEmp.Department;

            return Ok(objExistingEmp);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee objExistingEmp = _employees.FirstOrDefault(emp => emp.Id == id);

            if (objExistingEmp == null)
                return NotFound(); // Return 404 if employee not found

            _employees.Remove(objExistingEmp);
            return Ok(objExistingEmp);
        }
    }
}
