using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scope___Accessibility_modifier
{
    class Program : ProtectedMod
    {
        class AccessModifiers
        {
            public int num;
            //private AccessModifiers can only be used in same class
            int age;

            public AccessModifiers()
            {}

            public AccessModifiers(int inputAge)
            {
                age = inputAge;
                Console.WriteLine($"Private Age => {age}");
            }
        }

        static void Main(string[] args)
        {
            AccessModifiers modifiers = new AccessModifiers();
            modifiers.num = 11;
            Console.WriteLine($"Public Modifier => {modifiers.num}");

            AccessModifiers mods = new AccessModifiers(23);

            Program program = new Program();
            program.num2 = 22;
            Console.WriteLine($"Protected Number => {program.num2}");
            
            Console.ReadKey();
        }
    }

    class ProtectedMod
    {
        protected int num2;
    }
}
