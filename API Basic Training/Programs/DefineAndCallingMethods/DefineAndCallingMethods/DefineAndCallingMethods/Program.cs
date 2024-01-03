using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineAndCallingMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            NewClass newClass = new NewClass();
            Console.Write("Enter Your Name => ");
            string name = Console.ReadLine();
            string storedName = newClass.SetName(name);
            Console.WriteLine(storedName);
            Console.ReadKey();
        }
    }

    class NewClass
    {
        public string Name { get; set; }

        public NewClass()
        {
            Console.WriteLine("Inside New Class");
        }

        public string SetName(string name)
        {
            Name = name;
            return "Name Inside New Class => " + Name;
        }

    }

}
