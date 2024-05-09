using Lambda_Expression.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lambda_Expression.BL
{
    /// <summary>
    /// Employee BL File for Handling Employee CRUD.
    /// </summary>
    public class BLEmployee
    {
        #region Public Members
        /// <summary>
        /// A static list of employees for demonstration purposes.
        /// </summary>
        public static List<Employee> lstEmployees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Dimple Mithiya", Department = "Development", Salary = 50000 },
            new Employee { Id = 2, Name = "Pankaj Mithiya", Department = "Business", Salary = 75000 },
            new Employee { Id = 3, Name = "Ankit Bhanushali", Department = "Business Analytics", Salary = 80000 }
        };

        #endregion

        #region Public Methods
        /// <summary>
        /// Gets all employees.
        /// </summary>
        public List<Employee> GetAllEmployees()
        {
            return lstEmployees;
        }

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        public static Employee GetEmployeeById(int id)
        {
            if (id != null)
            {
                return lstEmployees.FirstOrDefault(emp => emp.Id == id);
            }
            return null;
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        public string AddEmployee(Employee objEmp)
        {
            if (objEmp != null)
            {
                objEmp.Id = lstEmployees.Count + 1;
                lstEmployees.Add(objEmp);
                return "Employee Added Successfully";
            }

            return "Error Occured ! Please try again";

        }

        /// <summary>
        /// Edits an existing employee.
        /// </summary>
        public Employee EditEmployee(int id, Employee objEmp)
        {
            if (id != null)
            {
                Employee objExistingEmp = BLEmployee.GetEmployeeById(id);
                objExistingEmp.Name = objEmp.Name;
                objExistingEmp.Salary = objEmp.Salary;
                objExistingEmp.Department = objEmp.Department;

                return objExistingEmp;
            }
            return null;

        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        public string DeleteEmployee(int id)
        {

            lstEmployees.Remove(BLEmployee.GetEmployeeById(id));
            return $"Employee with Id => {id} Deleted";

        }

        /// <summary>
        /// Gets all employees with salary greater than a specified amount.
        /// </summary>
        public List<Employee> GetEmployeesWithSalaryGreaterThan(int salary)
        {
            return lstEmployees.Where(emp => emp.Salary > salary).ToList();
        }

        /// <summary>
        /// Gets the highest paid employee.
        /// </summary>
        public Employee GetHighestPaidEmployee()
        {
            return lstEmployees.OrderByDescending(emp => emp.Salary).FirstOrDefault();
        }

        /// <summary>
        /// Gets the average salary of all employees.
        /// </summary>
        public double GetAverageSalary()
        {
            return lstEmployees.Average(emp => emp.Salary);
        }

        #endregion

    }
}
