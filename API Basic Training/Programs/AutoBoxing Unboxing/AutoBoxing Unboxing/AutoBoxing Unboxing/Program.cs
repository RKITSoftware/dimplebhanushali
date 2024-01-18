using System;

namespace AutoBoxing_Unboxing
{
    class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Boxing: Implicit type conversion (value type to reference type)
            int age = 23;
            object o = age;

            // Unboxing: Explicit type conversion (reference type to value type)
            int newAge = (int)o;

            // Output the results of boxing and unboxing
            Console.WriteLine($"Boxing => {o}");
            Console.WriteLine($"Unboxing => {newAge}");

            // Find and display data types

            // Output the data type of byte
            Console.WriteLine(typeof(byte));

            // Output the data type of int
            Console.WriteLine(typeof(int));

            // Output the data type of string
            Console.WriteLine(typeof(string));

            // Output the runtime data type of the boxed value (o) and the original value (age)
            Console.WriteLine(o.GetType());
            Console.WriteLine(age.GetType());

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
