using System;

namespace DefineAndCallingMethods
{
    /// <summary>
    /// This program demonstrates defining a class with a property, constructor, and method.
    /// It creates an instance of the class, prompts the user to enter a name, calls a method to set the name,
    /// and displays the result.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of NewClass
            NewClass newClass = new NewClass();

            // Prompt user to enter a name
            Console.Write("Enter Your Name => ");
            string name = Console.ReadLine();

            // Call the SetName method and store the result
            string storedName = newClass.SetName(name);

            // Display the result
            Console.WriteLine(storedName);

            Console.ReadKey();
        }
    }

    /// <summary>
    /// The NewClass class contains a property to store the name,
    /// a default constructor, and a method to set the name and return a formatted string.
    /// </summary>
    class NewClass
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default constructor for NewClass.
        /// </summary>
        public NewClass()
        {
            Console.WriteLine("Inside New Class");
        }

        /// <summary>
        /// Sets the name and returns a formatted string.
        /// </summary>
        /// <param name="name">The name to set.</param>
        /// <returns>A string containing the formatted name.</returns>
        public string SetName(string name)
        {
            Name = name;
            return "Name Inside New Class => " + Name;
        }
    }
}
