using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basic_Api.Models
{
    public class Student
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public static List<Student> lstStudents = new List<Student>
        {
            new Student { Id = 1,Name="Dimple Mithiya",Age=23,Course="MCA"},
            new Student { Id = 2,Name="Pankaj Mithiya",Age=28,Course="CE"},
            new Student { Id = 3,Name="Krishna Ram",Age=26,Course="MBA"},
            new Student { Id = 4,Name="Ankit Katarmal",Age=26,Course="MSC"},
            new Student { Id = 5,Name="Shiva Shakti",Age=32,Course="BBA"},
        };

    }
}