using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetic_Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            //Arithmetic Operators
            Console.Write("Enter Number of subjects => ");
            int subject = int.Parse(Console.ReadLine());

            int sum = 0;
            for (int i = 0; i < subject; i++)
            {
                Console.Write($"Enter Marks for subject {i+1} => ");
                int mark = int.Parse(Console.ReadLine());

                sum += mark; // Addition
            }

            float Percentage = (float)sum / (float)subject;   // Division

            Console.WriteLine($"Percentage of {subject} subjects => {Percentage}");

            int num1 = 11;
            int num2 = 23;

            Console.WriteLine($"Multiplication => {num1 * num2}");
            Console.WriteLine($"Substraction => {num1 - num2}");
            Console.WriteLine($"Modulus => {num2 % num1}");

            Console.ReadKey();

        }
    }
}
