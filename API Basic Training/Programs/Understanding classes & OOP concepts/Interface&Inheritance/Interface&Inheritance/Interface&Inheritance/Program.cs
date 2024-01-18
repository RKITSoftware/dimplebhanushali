using System;

namespace Interface_Inheritance
{
    class Program
    {
        /// <summary>
        /// Entry point of the program demonstrating interface inheritance.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.Write("Enter Name => ");
            string name = Console.ReadLine();
            Console.Write("Enter Age => ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter City => ");
            string city = Console.ReadLine();

            Console.WriteLine();

            Test test = new Test();
            test.DisplayName(name);
            test.DisplayCity(city);
            test.DisplayAge(age);

            Console.ReadKey();
        }
    }

    /// <summary>
    /// A class implementing the IDetails interface, demonstrating interface inheritance.
    /// </summary>
    class Test : IDetails
    {
        /// <summary>
        /// Displays the age.
        /// </summary>
        /// <param name="age">The age to be displayed.</param>
        public void DisplayAge(int age)
        {
            Console.WriteLine($"Age => {age}");
        }

        /// <summary>
        /// Displays the city.
        /// </summary>
        /// <param name="city">The city to be displayed.</param>
        public void DisplayCity(string city)
        {
            Console.WriteLine($"City => {city}");
        }

        /// <summary>
        /// Displays the name.
        /// </summary>
        /// <param name="name">The name to be displayed.</param>
        public void DisplayName(string name)
        {
            Console.WriteLine($"Name => {name}");
        }
    }

    /// <summary>
    /// An interface extending IPersonalDetail to include city details.
    /// </summary>
    public interface IDetails : IPersonalDetail
    {
        /// <summary>
        /// Displays the city.
        /// </summary>
        /// <param name="city">The city to be displayed.</param>
        void DisplayCity(string city);
    }

    /// <summary>
    /// An interface defining personal details such as name and age.
    /// </summary>
    public interface IPersonalDetail
    {
        /// <summary>
        /// Displays the name.
        /// </summary>
        /// <param name="name">The name to be displayed.</param>
        void DisplayName(string name);

        /// <summary>
        /// Displays the age.
        /// </summary>
        /// <param name="age">The age to be displayed.</param>
        void DisplayAge(int age);
    }
}
