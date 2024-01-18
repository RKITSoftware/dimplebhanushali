using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial_Class
{
    /// <summary>
    /// Main program to demonstrate the usage of a partial class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Create an instance of the partial class
            MyClass myObject = new MyClass();

            // Call methods from both parts
            myObject.MethodFromPart1();
            myObject.MethodFromPart2();

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
