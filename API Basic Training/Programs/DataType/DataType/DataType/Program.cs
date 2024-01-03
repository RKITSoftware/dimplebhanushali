using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType
{
    class Program
    {
        static void Main(string[] args)
        {
            //Simple Data Types
            int no= 68;
            float no2= 11.11f;
            char character = 'D';
            string name = "Dimple";
            bool isBool = true;

            // Data Type Conversion

            // Automatic Conversion
            float floatNum = no;
            long longNum = no;
            double doubleNum = floatNum;
            double doubleNum2 = longNum;

            // Explicit Conversion
            no = (int)longNum;
            floatNum = (float)doubleNum;

            Console.WriteLine("Byte Conversion => " + Convert.ToByte(no));
            Console.WriteLine("character Conversion => " + Convert.ToChar(no));
            Console.WriteLine("Double Conversion => " + Convert.ToDouble(no));
            Console.WriteLine("String Conversion => " + Convert.ToString(no));
            Console.WriteLine("Decimal Conversion => " + Convert.ToDecimal(no));

            Console.WriteLine();

            Console.WriteLine("Integer => " + no);
            Console.WriteLine("Float => " + no2);
            Console.WriteLine("character => " + character);
            Console.WriteLine("Boolean => " + isBool);
            Console.WriteLine("String => " + name);
            Console.WriteLine("Boolean => " + Convert.ToBoolean(1));
            Console.WriteLine("Boolean => " + Convert.ToBoolean(0));

            Console.ReadKey();

        }
    }
}