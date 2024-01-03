using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assignment operator
            int num1 = 10;

            Console.WriteLine($"Current Value of Number 1 => {num1}");

            num1 += 5;
            Console.WriteLine($"New Value of Number 1 (Add Assignment) => {num1}");

            num1 -= 5;
            Console.WriteLine($"New Value of Number 1 (Substract Assignment) => {num1}");

            num1 *= 5;
            Console.WriteLine($"New Value of Number 1 (Multiply Assignment) => {num1}");

            num1 /= 5;
            Console.WriteLine($"New Value of Number 1 (Division Assignment) => {num1}");

            num1 %= 3;
            Console.WriteLine($"New Value of Number 1 (Modulo Assignment) => {num1}");

            int num2 = 15;

            num2 <<= 2;
            Console.WriteLine($"New Value of Number 2 (Left Shift Assignment) => {num2}");

            num2 >>= 2;
            Console.WriteLine($"New Value of Number 2 (Right Shift Assignment) => {num2}");

            num2 ^= 2;
            Console.WriteLine($"New Value of Number 2 (Exclusive XOR Assignment) => {num2}");

            num2 &= 2;
            Console.WriteLine($"New Value of Number 2 (Bitwise AND Assignment) => {num2}");

            num2 |= 2;
            Console.WriteLine($"New Value of Number 2 (Bitwise OR Assignment) => {num2}");

            Console.ReadKey();
        }
    }
}
