using System;

namespace JaggedArray
{
    /// <summary>
    /// A program demonstrating the creation and display of a jagged array.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Get the number of rows for the jagged array from the user
            Console.Write("Enter the number of rows =>  ");
            int rows = int.Parse(Console.ReadLine());

            // Create the jagged array
            int[][] jaggedArray = new int[rows][];

            // Populate the jagged array with user input
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"Enter the number of elements for row {i + 1}: ");
                int col = int.Parse(Console.ReadLine());

                jaggedArray[i] = new int[col];

                for (int j = 0; j < col; j++)
                {
                    Console.Write($"Enter value for element at position [{i},{j}]: ");
                    jaggedArray[i][j] = int.Parse(Console.ReadLine());
                }
            }

            // Display the jagged array using foreach
            Console.WriteLine("Entered jagged array => ");

            foreach (int[] row in jaggedArray)
            {
                foreach (int element in row)
                {
                    Console.Write($"{element} \t");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
