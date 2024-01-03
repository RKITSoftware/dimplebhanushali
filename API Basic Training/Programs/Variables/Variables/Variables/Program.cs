using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variables
{
    class Program
    {
        //Static Value 
        //Member Variable
        static int value;
        static void Main(string[] args)
        {
            // Variables 

            int date = 9;
            string month;
            const int year = 2001;

            value = 22;
            date = 11;
            month = "January";

            Console.WriteLine($"date of Birth => {date} - {month} - {year}");
            Console.WriteLine($"Static Value => {value}");
            Console.ReadKey();

        }
    }
}
