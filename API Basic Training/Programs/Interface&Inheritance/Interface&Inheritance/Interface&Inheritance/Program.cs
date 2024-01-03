using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//interface & Interface_Inheritance 
namespace Interface_Inheritance
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            Console.Write("Enter Name => ");
            string name = Console.ReadLine();
            Console.Write("Enter Age => ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter City => ");
            string city = Console.ReadLine();

            Console.WriteLine();

            Test test = new Test();
            test.DisplayName(name);
            test.DisplayCity(city);
            test.DisplayAge(age);
            
            Console.ReadKey();
        }
    }
    class Test : IDetails
    {

        public void DisplayAge(int age)
        {
            Console.WriteLine($"Age => {age}");
        }

        public void DisplayCity(string city)
        {
            Console.WriteLine($"City => {city}");
        }

        public void DisplayName(string name)
        {
            Console.WriteLine($"Name => {name}");
        }
    }

    public interface IPersonalDetail
    {
        void DisplayName(string name);
        void DisplayAge(int age);
    }

    public interface IDetails : IPersonalDetail
    {
        void DisplayCity(string city);
    }
}