using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statements
{
    class Program
    {
        static void Main(string[] args)
        {
            // Statements 

            //if else if else statement
            Console.Write("Enter age => ");
            int age = int.Parse(Console.ReadLine());

            if (age > 0 && age <= 10)
            {
                Console.WriteLine("You Are Kid ");
            }
            else if (age > 10 && age < 20)
            {
                Console.WriteLine("You Are Teenager ");
            }
            else if (age >= 20 && age < 60)
            {
                Console.WriteLine("You Are Adult ");
            }
            else if (age >= 60)
            {
                Console.WriteLine("You Are Senior Citizen ");
            }
            else
            {
                Console.WriteLine("Please Enter Valid age :: ");
            }

            Console.ReadKey();

        }
    }
}
