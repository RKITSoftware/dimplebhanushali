using Interface.Interface;
using Interface.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Interface.Controllers
{
    /// <summary>
    /// Controller for managing employee data through Web API.
    /// </summary>
    public class EmployeeController : ApiController
    {
        private IEmployee<Employee> _employee = new EmployeeRepo();

        /// <summary>
        /// Gets all employees.
        /// </summary>
        [HttpGet]
        [Route("api/GetAll")]
        public IHttpActionResult GetAllEmployees()
        {
            IEnumerable<Employee> employees = _employee.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        [HttpGet]
        [Route("api/Employee/{id}")]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee employee = _employee.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employee">The employee object to be added.</param>
        [HttpPost]
        [Route("api/AddEmployee")]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            _employee.Add(employee);
            return Ok($"Employee {employee.Name} added successfully.");
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The ID of the employee to be updated.</param>
        /// <param name="updatedEmployee">The updated employee object.</param>
        [HttpPut]
        [Route("api/Update/{id}")]
        public IHttpActionResult UpdateEmployee(int id, Employee updatedEmployee)
        {
            _employee.Update(id, updatedEmployee);
            return Ok($"Employee with ID {id} updated successfully.");
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be deleted.</param>
        [HttpDelete]
        [Route("api/Delete/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _employee.Delete(id);
            return Ok($"Employee with ID {id} deleted successfully.");
        }
    }
}
