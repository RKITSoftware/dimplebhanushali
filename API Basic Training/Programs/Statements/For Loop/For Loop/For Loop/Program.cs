using System;

namespace For_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            // For Loop

            // Simple Star Pattern
            Console.WriteLine("Simple Star Pattern:");
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            // Hollow Diamond Pattern

            // First Half
            Console.WriteLine("Hollow Diamond Pattern (First Half):");
            for (int i = 0; i < 10; i++)
            {
                // Print leading stars
                for (int j = 10; j > i; j--)
                {
                    Console.Write("*");
                }
                // Print spaces
                for (int k = 0; k <= i * 2 - 1; k++)
                {
                    Console.Write(" ");
                }
                // Print trailing stars
                for (int l = 10; l > i; l--)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            // Second Half
            Console.WriteLine("Hollow Diamond Pattern (Second Half):");
            for (int i = 0; i < 10; i++)
            {
                // Print leading stars
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                // Print leading spaces
                for (int j = 9; j > i; j--)
                {
                    Console.Write(" ");
                }
                // Print trailing spaces
                for (int j = 9; j > i; j--)
                {
                    Console.Write(" ");
                }
                // Print trailing stars
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
