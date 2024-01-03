using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerations
{
    // Define an enumeration named 'Days'
    enum Days
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Using enum values
            Days today = Days.Wednesday;
            Console.WriteLine($"Today is {today}");

            // Enum iteration
            Console.WriteLine("\nEnum Iteration:");
            foreach (Days day in Enum.GetValues(typeof(Days)))
            {
                Console.WriteLine(day);
            }

            Console.ReadKey();
        }
    }
}
