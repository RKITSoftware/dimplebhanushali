using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaggedArray
{
    class Program
    {
        static void Main(string[] args)
        {
            //JaggedArray Array

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