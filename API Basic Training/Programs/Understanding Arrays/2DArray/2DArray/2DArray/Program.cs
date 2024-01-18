using System;

namespace _2DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaration of variables to store the number of rows and columns
            Console.Write("Enter Number of Rows => ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Enter Number of Columns => ");
            int column = int.Parse(Console.ReadLine());

            // Declaration and initialization of the 2D array
            int[,] arr = new int[row, column];

            // Input values for each element in the 2D array
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"Enter Value for [{i}][{j}] => ");
                    arr[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // Display the 2D array
            Console.WriteLine("2D array:");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"{arr[i, j]} \t");
                }
                Console.WriteLine();
            }

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
