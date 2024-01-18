using System;

namespace Do_While_Loop
{
    /// <summary>
    /// This program generates a hollow diamond pattern using nested do-while loops.
    /// </summary>
    class Program
    {
        static void Main()
        {
            int i = 0;

            // First Half of the Diamond
            do
            {
                // Print left half of the diamond
                int j = 10;
                do
                {
                    Console.Write("*");
                    j--;
                } while (j > i);

                // Print spaces between the stars
                int k = 0;
                do
                {
                    Console.Write(" ");
                    k++;
                } while (k <= i * 2 - 1);

                // Print right half of the diamond
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

            // Second Half of the Diamond
            do
            {
                // Print left half of the diamond
                int j = 0;
                do
                {
                    Console.Write("*");
                    j++;
                } while (j <= i);

                // Print spaces between the stars
                int space1 = 9;
                do
                {
                    Console.Write(" ");
                    space1--;
                } while (space1 > i);

                // Print spaces between the stars
                int space2 = 9;
                do
                {
                    Console.Write(" ");
                    space2--;
                } while (space2 >= i);

                // Print right half of the diamond
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
