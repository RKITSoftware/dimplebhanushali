using Lambda_Expression.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambda_Expression.BL
{
    public class BLEmployee
    {
        public List<Employee> GetAllEmployees()
        {
            return Employee.lstEmployees;
        }

        public static Employee GetEmployeeById(int id)
        {
            if (id != null)
            {
                return Employee.lstEmployees.FirstOrDefault(emp => emp.Id == id);
            }
            return null;
        }

        public string AddEmployee(Employee objEmp)
        {
            try
            {
                if (objEmp != null)
                {
                    objEmp.Id = Employee.lstEmployees.Count + 1;
                    Employee.lstEmployees.Add(objEmp);
                    return "Employee Added Successfully";
                }
                else
                {
                    return "Error Occured ! Please try again";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Employee EditEmployee(int id, Employee objEmp)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public string DeleteEmployee(int id)
        {
            try
            {
                Employee.lstEmployees.Remove(BLEmployee.GetEmployeeById(id));
                return $"Employee with Id => {id} Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}