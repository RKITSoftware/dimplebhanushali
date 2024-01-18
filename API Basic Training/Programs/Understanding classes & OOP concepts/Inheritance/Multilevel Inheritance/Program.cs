using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multilevel_Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the base class (Person)
            Person person = new Person("Pankaj Mithiya", 25);
            Console.WriteLine("Person Information:");
            person.DisplayInfo();
            Console.WriteLine();

            // Create an instance of the derived class (Student)
            Student student = new Student("Dimple Mithiya", 20, "S12345");
            Console.WriteLine("Student Information:");
            student.DisplayInfo();
            Console.WriteLine();

            // Create an instance of the further derived class (GraduateStudent)
            GraduateStudent objGradStudent = new GraduateStudent("Dimple Mithiya", 28, "GS6789", "Advanced Topics in Computer Science");
            Console.WriteLine("Graduate Student Information:");
            objGradStudent.DisplayInfo();

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
            Console.WriteLine($"Person: Name: {Name}, Age: {Age}");
        }
    }

    /// <summary>
    /// The derived class representing a student.
    /// </summary>
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
            Console.WriteLine($"Student: Name: {Name}, Age: {Age}, Student ID: {StudentId}");
        }
    }

    /// <summary>
    /// The derived class representing a graduate student.
    /// </summary>
    public class GraduateStudent : Student
    {
        /// <summary>
        /// Gets or sets the thesis topic.
        /// </summary>
        public string ThesisTopic { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraduateStudent"/> class.
        /// </summary>
        /// <param name="name">The name of the graduate student.</param>
        /// <param name="age">The age of the graduate student.</param>
        /// <param name="studentId">The student ID of the graduate student.</param>
        /// <param name="thesisTopic">The thesis topic of the graduate student.</param>
        public GraduateStudent(string name, int age, string studentId, string thesisTopic)
            : base(name, age, studentId)
        {
            ThesisTopic = thesisTopic;
        }

        /// <summary>
        /// Displays information about the graduate student.
        /// Overrides the base class method.
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"Graduate Student: Name: {Name}, Age: {Age}, Student ID: {StudentId}, Thesis Topic: {ThesisTopic}");
        }
    }
}
