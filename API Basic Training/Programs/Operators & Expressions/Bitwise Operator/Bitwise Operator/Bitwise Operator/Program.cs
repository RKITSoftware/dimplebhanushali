using System;

namespace Bitwise_Operator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bitwise Operator demonstration
            int num1 = 10, num2 = 15, result;

            // Bitwise AND Operator
            result = num1 & num2;
            Console.WriteLine($"Bitwise &(AND) Operator =>  {result}");

            // Bitwise OR Operator
            result = num1 | num2;
            Console.WriteLine($"Bitwise |(OR) Operator =>  {result}");

            // Bitwise XOR Operator
            result = num1 ^ num2;
            Console.WriteLine($"Bitwise ^(XOR) Operator =>  {result}");

            // Bitwise NOT Operator
            result = ~num2;
            Console.WriteLine($"Bitwise ~ Operator =>  {result}");

            // Bitwise Right Shift Operator
            result = num2 >> 2;
            Console.WriteLine($"Bitwise >> (Right Shift) Operator =>  {result}");

            // Bitwise Left Shift Operator
            result = num2 << 2;
            Console.WriteLine($"Bitwise << (Left Shift) Operator =>  {result}");

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
