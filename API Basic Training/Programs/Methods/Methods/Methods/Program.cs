using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            //Methods

            Console.Write("Enter Number of Elements => ");
            int no = int.Parse(Console.ReadLine());

            int[] arr = new int[no];

            for (int i = 0; i < no; i++)
            {
                Console.Write("Enter Element => ");
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            //Calling Method
            int max = MaxNumber(arr);
            Console.WriteLine($"Maximum From Above Array is => {max}");

            Console.ReadKey();

        }

        //Defining Method
        static int MaxNumber(int[] numbers)
        {
            int max = numbers[0];

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }        
    }
}
