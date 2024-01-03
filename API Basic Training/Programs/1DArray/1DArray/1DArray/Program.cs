using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            //1D Array
            string[] cars = { "Lamborghini","Audi","BMW","Mercedes"};
            int[] numbers = { 1,2,11,22,10,36};

            Console.Write("cars => ");
            foreach (var car in cars)
            {
                Console.Write($"{car} ");
            }
            Console.WriteLine();
            Console.Write("numbers  => ");
             foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.ReadKey();
        }
    }
}
