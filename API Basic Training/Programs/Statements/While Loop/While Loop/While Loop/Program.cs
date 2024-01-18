using System;

namespace While_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            // while Loop
            // Hollow Diamond Pattern

            int i = 0;

            // First Half 
            while (i < 10)
            {
                int j = 10;
                while (j > i)
                {
                    Console.Write("*");
                    j--;
                }

                int k = 0;
                while (k <= i * 2 - 1)
                {
                    Console.Write(" ");
                    k++;
                }

                int l = 10;
                while (l > i)
                {
                    Console.Write("*");
                    l--;
                }

                Console.WriteLine();
                i++;
            }

            i = 0;

            // Second Half 
            while (i < 10)
            {
                int j = 0;
                while (j <= i)
                {
                    Console.Write("*");
                    j++;
                }

                int space1 = 9;
                while (space1 > i)
                {
                    Console.Write(" ");
                    space1--;
                }

                int space2 = 9;
                while (space2 > i)
                {
                    Console.Write(" ");
                    space2--;
                }

                int k = 0;
                while (k <= i)
                {
                    Console.Write("*");
                    k++;
                }

                Console.WriteLine();
                i++;
            }

            Console.ReadKey();
        }
    }
}
