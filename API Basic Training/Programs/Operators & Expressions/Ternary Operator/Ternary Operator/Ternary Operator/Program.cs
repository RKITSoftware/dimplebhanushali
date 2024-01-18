using System;

namespace TernaryOperator
{
    class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Ternary Operator
            Console.Write("Enter Your age => ");
            int age = int.Parse(Console.ReadLine());

            // Ternary operator to determine if the person is eligible to vote
            string message = age >= 18 ? "Congratulations You Can Vote Now !!! " : "Oops !! You Are not Eligible ";

            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
