using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Static_Class
{
    public class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        static void Main(string[] args)
        {
            // Accessing static members without creating an instance of the class
            int result = MathUtility.Add(5, 3);
            Console.WriteLine("Result of addition: " + result);

            int multipliedResult = MathUtility.MultiplyByTwo;
            Console.WriteLine("Result multiplied by two: " + multipliedResult);
                
            // Accessing the static field
            Console.WriteLine("Counter value: " + MathUtility.Counter);

            // Static members are accessed using the class name, not an instance
            // MathUtility utilityInstance = new MathUtility(); // This would result in a compilation error

            Console.ReadLine();
        }
    }
}
