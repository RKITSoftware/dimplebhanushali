using System;

namespace Logical_Operator
{
    /// <summary>
    /// A program demonstrating the usage of logical operators.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Logical Operator
            bool result;
            bool True = true;
            bool False = false;

            // Logical AND Operator
            result = True && False;
            Console.WriteLine($"AND Operator => {result}");

            result = True && True;
            Console.WriteLine($"AND Operator => {result}");

            result = False && False;
            Console.WriteLine($"AND Operator => {result}");

            // Logical OR Operator
            result = True || False;
            Console.WriteLine($"OR Operator => {result}");

            // Logical NOT Operator
            result = !True;
            Console.WriteLine($"NOT Operator => {result}");

            result = !False;
            Console.WriteLine($"NOT Operator => {result}");

            Console.ReadKey();
        }
    }
}
