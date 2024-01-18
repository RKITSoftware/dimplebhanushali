using System;

namespace Methods
{
    /// <summary>
    /// A program demonstrating the usage of methods to find the maximum number in an array.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            Console.Write("Enter Number of Elements => ");
            int no = int.Parse(Console.ReadLine());

            int[] arr = new int[no];

            for (int i = 0; i < no; i++)
            {
                Console.Write("Enter Element => ");
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            // Calling Method
            int max = MaxNumber(arr);
            Console.WriteLine($"Maximum From Above Array is => {max}");

            Console.ReadKey();
        }

        /// <summary>
        /// Method to find the maximum number in an array.
        /// </summary>
        /// <param name="numbers">Array of integers.</param>
        /// <returns>The maximum number in the array.</returns>
        static int MaxNumber(int[] numbers)
        {
            int max = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }
    }
}
