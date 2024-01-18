using System;

namespace _1DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declaration and initialization of string array "cars"
            string[] cars = { "Lamborghini", "Audi", "BMW", "Mercedes" };

            // Declaration and initialization of integer array "numbers"
            int[] numbers = { 1, 2, 11, 22, 10, 36 };

            // Displaying the contents of the "cars" array
            Console.Write("cars => ");
            foreach (var car in cars)
            {
                Console.Write($"{car} ");
            }
            Console.WriteLine();

            // Displaying the contents of the "numbers" array
            Console.Write("numbers  => ");
            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
