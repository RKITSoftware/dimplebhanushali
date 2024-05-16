using System.Collections.Generic;
using System.Linq;
using Token_Auth.Models;

namespace Token_Auth.BL
{
    public class EmployeeBL
    {
        public Employee GetEmployeeById(int id)
        {
            return Employee.lstEmployees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> GetSomeEmployees()
        {
            return Employee.lstEmployees.Take(2);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Employee.lstEmployees.ToList();
        }
    }
}