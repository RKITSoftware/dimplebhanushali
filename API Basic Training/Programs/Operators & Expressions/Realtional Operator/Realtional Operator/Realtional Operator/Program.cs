using System;

namespace Relational_Operator
{
    /// <summary>
    /// A program demonstrating the usage of relational operators.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Relational Operator
            int num = 11;
            int num2 = 22;

            bool result;

            Console.WriteLine($"number 1 => {num}");
            Console.WriteLine($"number 2 => {num2}");

            // Equal to (==) Operator
            result = num == num2;
            Console.WriteLine($" '==' Operator => {result}");

            // Not Equal to (!=) Operator
            result = num != num2;
            Console.WriteLine($" '!=' Operator => {result}");

            // Greater than or Equal to (>=) Operator
            result = num >= num2;
            Console.WriteLine($" '>=' Operator => {result}");

            // Less than or Equal to (<=) Operator
            result = num <= num2;
            Console.WriteLine($" '<=' Operator => {result}");

            // Less than (<) Operator
            result = num < num2;
            Console.WriteLine($" '<' Operator => {result}");

            // Greater than (>) Operator
            result = num > num2;
            Console.WriteLine($" '>' Operator => {result}");

            Console.ReadKey();
        }
    }
}
