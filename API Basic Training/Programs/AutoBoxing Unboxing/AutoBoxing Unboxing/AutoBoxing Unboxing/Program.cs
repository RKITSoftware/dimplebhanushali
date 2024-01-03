using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBoxing_Unboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            // Boxing => Implicit Type Conversion
            int age = 23;
            object o = age;

            //Unboxing => Explicit Type Conversion
            int newAge = (int)o;

            // Find Data Types

            //Type type = age.GetType();

            Console.WriteLine($"Boxing => {o}");
            Console.WriteLine($"UnBoxing => {newAge}");

            Console.WriteLine(typeof(byte));
            Console.WriteLine(typeof(int));
            Console.WriteLine(typeof(string));
    
            Console.WriteLine(o.GetType());
            Console.WriteLine(age.GetType());
            Console.ReadKey();

        }
    }
}
