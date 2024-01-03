using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Understanding Classes and OOP

namespace Classes_OOP
{
    //class Members

    class Program
    {
        static void Main(string[] args)
        {
            NewClass.Display();
            NewClass newClass = new NewClass();
            NewClass newClass1 = new NewClass();
            Console.ReadKey();
        }
    }

    class NewClass
    {
        public string Name { get; set; }

        //Instance Constructor
        public NewClass()
        {
            Console.WriteLine("Instance Constructor");            
        }

        //class Constructor
        static NewClass()
        {
            Console.WriteLine("Class Constructor");
        }

        //Instance Methods
        public void Show()
        {
            Console.WriteLine("Hello World !!!");
        }

        //Class Methods
        public static void Display()
        {
            Console.WriteLine("Hello World From Class Method!!!");
        }

    }

}