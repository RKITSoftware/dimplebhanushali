using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Methods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize an array
            int[] numbers = { 1, 2, 3, 4, 5 };

            // Display the original array
            Console.WriteLine("Original Array:");
            DisplayArray(numbers);

            // Length of the array
            Console.WriteLine($"\nLength of the Array: {numbers.Length}");

            // Sorting the array
            Array.Sort(numbers);
            Console.WriteLine("\nSorted Array:");
            DisplayArray(numbers);

            // Reversing the array
            Array.Reverse(numbers);
            Console.WriteLine("\nReversed Array:");
            DisplayArray(numbers);

            // Searching for an element
            int searchValue = 4;
            int index = Array.IndexOf(numbers, searchValue);
            Console.WriteLine($"\nSearch for {searchValue}: Found at index {index}");

            // Copying elements to a new array
            int[] newArray = new int[numbers.Length];
            Array.Copy(numbers, newArray, numbers.Length);
            Console.WriteLine("\nCopied Array:");
            DisplayArray(newArray);

            // Resizing the array
            Array.Resize(ref newArray, 3);
            Console.WriteLine("\nResized Array:");
            DisplayArray(newArray);

            Console.ReadKey();

        }

        static void DisplayArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
