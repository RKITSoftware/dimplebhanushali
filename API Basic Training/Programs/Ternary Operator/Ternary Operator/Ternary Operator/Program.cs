using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ternary_Operator
{
    class Program
    {
        static void Main(string[] args)
        {

            //Ternary Operator
            Console.Write("Enter Your age => ");
            int age = int.Parse(Console.ReadLine());

            string Message = age >= 18 ? "Congratulations You Can Vote Now !!! " : "oops !! You Are not Eligible ";

            Console.WriteLine(Message);
            Console.ReadKey();

        }
    }
}
