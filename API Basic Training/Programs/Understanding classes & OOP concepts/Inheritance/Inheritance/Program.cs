using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the base class (Person)
            Person person = new Person("John Doe", 25);
            Console.WriteLine("Person Information:");
            person.DisplayInfo();
            Console.WriteLine();

            // Create an instance of the derived class (Student)
            Student student = new Student("Dimple Mithiya", 20, "S12345");
            Console.WriteLine("Student Information:");
            student.DisplayInfo();

            Console.ReadKey();
        }
    }

    public class Person
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; } 

        /// <summary>
        /// Gets or sets the age of the person.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="age">The age of the person.</param>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        /// <summary>
        /// Displays information about the person.
        /// </summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

    public class Student : Person
    {
        /// <summary>
        /// Gets or sets the student ID.
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        /// <param name="name">The name of the student.</param>
        /// <param name="age">The age of the student.</param>
        /// <param name="studentId">The student ID.</param>
        public Student(string name, int age, string studentId)
            : base(name, age)
        {
            StudentId = studentId;
        }

        /// <summary>
        /// Displays information about the student.
        /// Overrides the base class method.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Student ID: {StudentId}");
        }
    }
}
