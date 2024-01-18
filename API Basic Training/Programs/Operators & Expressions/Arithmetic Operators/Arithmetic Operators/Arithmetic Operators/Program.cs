using System;

namespace Arithmetic_Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            // Requesting the number of subjects from the user
            Console.Write("Enter Number of subjects => ");
            int subject = int.Parse(Console.ReadLine());

            // Initializing a variable to store the sum of marks
            int sum = 0;

            // Loop to input marks for each subject and calculate the sum
            for (int i = 0; i < subject; i++)
            {
                Console.Write($"Enter Marks for subject {i + 1} => ");
                int mark = int.Parse(Console.ReadLine());

                sum += mark; // Addition
            }

            // Calculating the percentage using floating-point division
            float Percentage = (float)sum / (float)subject; // Division

            // Displaying the calculated percentage
            Console.WriteLine($"Percentage of {subject} subjects => {Percentage}");

            // Using arithmetic operators on two numbers
            int num1 = 11;
            int num2 = 23;

            // Displaying the results of multiplication, subtraction, and modulus
            Console.WriteLine($"Multiplication => {num1 * num2}");
            Console.WriteLine($"Subtraction => {num1 - num2}");
            Console.WriteLine($"Modulus => {num2 % num1}");

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
