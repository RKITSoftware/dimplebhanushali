using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_While_Loop
{
    class Program
    {
        static void Main()
        {
            // Hollow Diamond Pattern

            int i = 0;

            // First Half 
            do
            {
                int j = 10;
                do
                {
                    Console.Write("*");
                    j--;
                } while (j > i);

                int k = 0;
                do
                {
                    Console.Write(" ");
                    k++;
                } while (k <= i * 2 - 1);

                int l = 10;
                do
                {
                    Console.Write("*");
                    l--;
                } while (l > i);

                Console.WriteLine();
                i++;
            } while (i < 10);

            i = 0;

            // Second Half 
                    do
        {
            int j = 0;
            do
            {
                Console.Write("*");
                j++;
            } while (j <= i);

            int space1 = 9;
            do
            {
                Console.Write(" ");
                space1--;
            } while (space1 > i);

            int space2 = 9;
            do
            {
                Console.Write(" ");
                space2--;
            } while (space2 >= i);

            int k = 0;
            do
            {
                Console.Write("*");
                k++;
            } while (k <= i);

            Console.WriteLine();
            i++;
        } while (i < 10);
            Console.ReadKey();
        }
    }
}
