using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitwise_Operator
{
    class Program
    {
        static void Main(string[] args)
        {

            //Bitwise Operator
            int num1 = 10, num2 = 15, result;

            result = num1 & num2;
            Console.WriteLine($"Bitwise &(AND) Operator =>  {result}");

            result = num1 | num2;
            Console.WriteLine($"Bitwise |(OR) Operator =>  {result}");

            result = num1 ^ num2;
            Console.WriteLine($"Bitwise ^(XOR) Operator =>  {result}");

            result = ~num2;
            Console.WriteLine($"Bitwise ~ Operator =>  {result}");

            result = num2 >> 2;
            Console.WriteLine($"Bitwise >> (Right Shift) Operator =>  {result}");

            result = num2 << 2;
            Console.WriteLine($"Bitwise << (Left Shift) Operator =>  {result}");

            Console.ReadKey();

        }
    }
}
