using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realtional_Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Realtional Operator
            int num = 11;
            int num2 = 22;

            bool result;

            Console.WriteLine($"number 1 => {num}");
            Console.WriteLine($"number 2 => {num2}");

            result = num == num2;
            Console.WriteLine($" '==' Operator => {result}");

            result = num != num2;
            Console.WriteLine($" '!=' Operator => {result}");

            result = num >= num2;
            Console.WriteLine($" '>=' Operator => {result}");

            result = num <= num2;
            Console.WriteLine($" '<=' Operator => {result}");

            result = num < num2;
            Console.WriteLine($" '<' Operator => {result}");

            result = num > num2;
            Console.WriteLine($" '>' Operator => {result}");

            Console.ReadKey();
        }
    }
}
