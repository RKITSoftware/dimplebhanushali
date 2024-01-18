using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Student class
            Student objStudent = new Student("Alice", 20, "S12345");

            // Display personal details using IPersonalDetails interface
            Console.WriteLine("Personal Details:");
            objStudent.DisplayPersonalDetails();

            Console.WriteLine();

            // Display academic details using IAcademicDetails interface
            Console.WriteLine("Academic Details:");
            objStudent.DisplayAcademicDetails();

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Interface representing personal details.
    /// </summary>
    public interface IPersonalDetails
    {
        string Name { get; set; }
        int Age { get; set; }

        void DisplayPersonalDetails();
    }

    /// <summary>
    /// Interface representing academic details.
    /// </summary>
    public interface IAcademicDetails
    {
        string StudentId { get; set; }

        void DisplayAcademicDetails();
    }

    /// <summary>
    /// Class representing a student implementing both personal and academic details.
    /// </summary>
    public class Student : IPersonalDetails, IAcademicDetails
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string StudentId { get; set; }

        public Student(string name, int age, string studentId)
        {
            Name = name;
            Age = age;
            StudentId = studentId;
        }

        public void DisplayPersonalDetails()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }

        public void DisplayAcademicDetails()
        {
            Console.WriteLine($"Student ID: {StudentId}");
        }
    }   
}
