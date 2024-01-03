using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            //For Loop

            //Simple Star Pattern
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("* ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            // Hollow Diamond Pattern

            //First Half 
            for (int i = 0; i < 10; i++)
            {
                for (int j = 10; j > i; j--)
                {
                    Console.Write("*");
                }
                for (int k = 0; k <= i*2-1; k++)
                {
                    Console.Write(" ");
                }
                for (int l = 10; l > i; l--)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }

            //Second Half 
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                for (int j = 9; j > i; j--)
                {
                    Console.Write(" ");
                }
                for (int j = 9; j > i; j--)
                {
                    Console.Write(" ");
                }
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
