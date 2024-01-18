using System;

namespace Arithmetic_Operators
{
    class Program
    {
        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Declare and initialize variable 'a'
            int a = 21;

            // Declare variable 'c'
            int c;

            // Assignment operator
            c = a;
            Console.WriteLine("Line 1 - =  Value of c = {0}", c);

            // Addition assignment operator
            c += a;
            Console.WriteLine("Line 2 - += Value of c = {0}", c);

            // Subtraction assignment operator
            c -= a;
            Console.WriteLine("Line 3 - -=  Value of c = {0}", c);

            // Multiplication assignment operator
            c *= a;
            Console.WriteLine("Line 4 - *=  Value of c = {0}", c);

            // Division assignment operator
            c /= a;
            Console.WriteLine("Line 5 - /=  Value of c = {0}", c);

            // Reset value of 'c'
            c = 200;

            // Modulus assignment operator
            c %= a;
            Console.WriteLine("Line 6 - %=  Value of c = {0}", c);

            // Left shift assignment operator
            c <<= 2;
            Console.WriteLine("Line 7 - <<=  Value of c = {0}", c);

            // Right shift assignment operator
            c >>= 2;
            Console.WriteLine("Line 8 - >>=  Value of c = {0}", c);

            // Bitwise AND assignment operator
            c &= 2;
            Console.WriteLine("Line 9 - &=  Value of c = {0}", c);

            // Bitwise XOR assignment operator
            c ^= 2;
            Console.WriteLine("Line 10 - ^=  Value of c = {0}", c);

            // Bitwise OR assignment operator
            c |= 2;
            Console.WriteLine("Line 11 - |=  Value of c = {0}", c);

            // Pause console to view output
            Console.ReadLine();
        }
    }
}
