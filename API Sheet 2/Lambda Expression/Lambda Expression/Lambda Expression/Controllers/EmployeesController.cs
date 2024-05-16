using Lambda_Expression.BL;
using Lambda_Expression.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Lambda_Expression.Controllers
{
    /// <summary>
    /// Employee Controleer for Handling Employee CRUD.
    /// </summary>
    [RoutePrefix("api")]
    public class EmployeesController : ApiController
    {
        /// <summary>
        /// Instance of BLEmployee.
        /// </summary>
        private BLEmployee _employees = new BLEmployee();

        /// <summary>
        /// Gets all employees.
        /// </summary>
        [HttpGet]
        [Route("GetAllEmployees")]
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees.GetAllEmployees();
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        [HttpGet]
        [Route("Employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee objEmp = BLEmployee.GetEmployeeById(id);

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
            _employees.AddEmployee(objEmp);

            return Ok(objEmp);
        }

        /// <summary>
        /// Edits an existing employee.
        /// </summary>
        [HttpPut]
        [Route("EditEmployee/{id}")]
        public IHttpActionResult EditEmployee(int id, Employee objEmp)
        {
            _employees.EditEmployee(id,objEmp);
            return Ok(objEmp);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _employees.DeleteEmployee(id);
            return Ok(BLEmployee.GetEmployeeById(id));
        }

        /// <summary>
        /// Gets all employees with salary greater than a specified amount.
        /// </summary>
        [HttpGet]
        [Route("GetEmployeesWithSalaryGreaterThan/{salary}")]
        public IEnumerable<Employee> GetEmployeesWithSalaryGreaterThan(int salary)
        {
            return _employees.GetEmployeesWithSalaryGreaterThan(salary);
        }

        /// <summary>
        /// Gets the highest paid employee.
        /// </summary>
        [HttpGet]
        [Route("GetHighestPaidEmployee")]
        public IHttpActionResult GetHighestPaidEmployee()
        {
            Employee highestPaidEmployee = _employees.GetHighestPaidEmployee();

            if (highestPaidEmployee == null)
                return NotFound(); // Return 404 if no employee found

            return Ok(highestPaidEmployee);
        }

        /// <summary>
        /// Gets the average salary of all employees.
        /// </summary>
        [HttpGet]
        [Route("GetAverageSalary")]
        public IHttpActionResult GetAverageSalary()
        {
            double averageSalary = _employees.GetAverageSalary();

            return Ok(averageSalary);
        }
    }
}
