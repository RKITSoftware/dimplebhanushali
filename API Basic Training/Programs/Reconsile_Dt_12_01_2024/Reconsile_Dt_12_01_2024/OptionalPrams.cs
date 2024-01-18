using System;

namespace Reconsile_Dt_12_01_2024
{
    /// <summary>
    /// Represents a class with methods demonstrating the use of optional parameters.
    /// </summary>
    public class OptionalParams
    {
        /// <summary>
        /// Displays information with default or specified name and age.
        /// </summary>
        /// <param name="name">The name to display (default is "DefaultName").</param>
        /// <param name="age">The age to display (default is 23).</param>
        public void DisplayInfo(string name = "DefaultName", int age = 23)
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }

        /// <summary>
        /// Displays information with default or specified ID and city.
        /// </summary>
        /// <param name="id">The ID to display (default is 11).</param>
        /// <param name="city">The city to display (default is "Rajkot").</param>
        public void DisplayInfo(int id = 11, string city = "Rajkot")
        {
            Console.WriteLine($"ID: {id}, City: {city}");
        }
    }
}
