using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Token_Auth.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public static List<Employee> lstEmployees = new List<Employee>
        {
            new Employee { Id=1, FirstName="Dimple",LastName="Mithiya",Email="abc@gmail.com",Gender="Female"},
            new Employee { Id=2, FirstName="Ankit",LastName="Katarmal",Email="xyz@gmail.com",Gender="Male"},
            new Employee { Id=3, FirstName="Shiva",LastName="Shakti",Email="shiva@gmail.com",Gender="Male"},
        };

    }
}