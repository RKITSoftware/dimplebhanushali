using System;

namespace ConditionalStatements
{
    class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Conditional Statements

            // if-else if-else statement
            Console.Write("Enter age => ");
            int age = int.Parse(Console.ReadLine());

            if (age > 0 && age <= 10)
            {
                Console.WriteLine("You Are a Kid");
            }
            else if (age > 10 && age < 20)
            {
                Console.WriteLine("You Are a Teenager");
            }
            else if (age >= 20 && age < 60)
            {
                Console.WriteLine("You Are an Adult");
            }
            else if (age >= 60)
            {
                Console.WriteLine("You Are a Senior Citizen");
            }
            else
            {
                Console.WriteLine("Please Enter a Valid Age");
            }

            Console.ReadKey();
        }
    }
}
