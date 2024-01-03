using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logical_Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Logical Operator
            bool result;
            bool True = true;
            bool False = false;

            result = True && False;
            Console.WriteLine($"AND Operator => {result}");

            result = True && True;
            Console.WriteLine($"AND Operator => {result}");

            result = False && False;
            Console.WriteLine($"AND Operator => {result}");

            result = True || False;
            Console.WriteLine($"OR Operator => {result}");

            result = !True;
            Console.WriteLine($"NOT Operator => {result}");

            result = !False;
            Console.WriteLine($"NOT Operator => {result}");

            Console.ReadKey();
        }
    }
}
