using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            //2DArray

            Console.Write("Enter Number of Rows => ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Enter Number of Columns => ");
            int column = int.Parse(Console.ReadLine());

            int[,] arr = new int[row,column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"Enter Value for [{i}][{j}] => ");
                    arr[i,j] = int.Parse(Console.ReadLine()); 
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
            Console.ReadKey();
        }
    }
}
